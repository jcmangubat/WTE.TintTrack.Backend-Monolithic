using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SMEAppHouse.Core.CodeKits;
using System.Linq.Expressions;
using System.Reflection;
using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Application.Shared.Validator;

public abstract class AutoValidator<T> : AbstractValidator<T>
{
    private readonly PasswordOptions _passwordOptions;

    protected AutoValidator(IOptions<IdentityOptions> identityOptions)
    {
        _passwordOptions = identityOptions.Value.Password;

        var properties = typeof(T).GetProperties();
        var nullabilityContext = new NullabilityInfoContext();

        foreach (var property in properties)
        {
            // Get nullability information for the property
            var nullabilityInfo = nullabilityContext.Create(property);
            var actualType = nullabilityInfo.ReadState == NullabilityState.Nullable ?
                                Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType :
                                property.PropertyType;

            // Create a lambda expression that directly accesses the property
            var propertyAccessExpr = CreatePropertyExpression<T>(property, actualType);

            CheckRequiredAttribute(property, propertyAccessExpr, actualType);
            /*var requiredAttr = property.GetCustomAttributes<RequiredAttribute>().FirstOrDefault();
            if (requiredAttr != null)
            {
                *//*RuleFor((Expression<Func<T, string>>)propertyAccessExpr)
                    .NotEmpty()
                    .WithMessage(requiredAttr.Message);*//*

                ApplyRequiredValidation(property, actualType, requiredAttr.Message);
            }*/


            CheckMaxLengthAttribute(property, propertyAccessExpr, actualType);
            CheckMinLengthAttribute(property, propertyAccessExpr, actualType);
            CheckEmailAttribute(property, propertyAccessExpr, actualType);
            CheckRangeAttribute(property, propertyAccessExpr, actualType);
            CheckCompareAttribute(property, properties, propertyAccessExpr, actualType);
            CheckCreditCardAttribute(property, propertyAccessExpr, actualType);
            CheckRegularExpressionAttribute(property, propertyAccessExpr, actualType);
            CheckUrlAttribute(property, propertyAccessExpr, actualType);
            CheckPasswordAttribute(property, propertyAccessExpr, actualType);
            CheckPhoneNumberAttribute(property, propertyAccessExpr, actualType);
        }
    }

    private void CheckPhoneNumberAttribute(PropertyInfo property, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var phoneNumberAttr = property.GetCustomAttributes<PhoneAttribute>().FirstOrDefault();
        if (phoneNumberAttr == null)
            return;

        if (actualType != typeof(string))
            throw new Exception("PhoneNumber attribute is only applicable to properties of type String.");

        // International phone number regex pattern
        string phoneNumberPattern = @"^\+?[1-9]\d{0,2}[\s\-]?\(?\d{1,4}\)?[\s\-]?\d{1,4}[\s\-]?\d{1,9}$";

        RuleFor(x => (string?)property.GetValue(x))
            .Matches(phoneNumberPattern)
            .WithMessage(phoneNumberAttr.Message);
    }

    private void CheckPasswordAttribute(PropertyInfo property, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var pswdAttr = property.GetCustomAttributes<PasswordAttribute>().FirstOrDefault();
        if (pswdAttr == null)
            return;

        if (actualType != typeof(string))
            throw new Exception("Password attribute is only applicable to properties of type String.");

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(parameter, property);
        var convertedProperty = Expression.Convert(propertyAccess, typeof(string)); // Convert to object to handle any type

        // Create the property expression: x => x.Property
        var expression = Expression.Lambda<Func<T, string>>(convertedProperty, parameter);

        // Use the expression directly in RuleFor to preserve PropertyName
        var rule = RuleFor(expression)
                        .NotEmpty()
                        .WithMessage(pswdAttr.Message);

        //var rule = RuleFor((Expression<Func<T, string>>)propertyAccessExpr).NotEmpty().WithMessage("Password is required.");

        // Dynamically apply password options
        if (_passwordOptions.RequiredLength > 0)
            rule = rule.MinimumLength(_passwordOptions.RequiredLength).WithMessage($"Password must be at least {_passwordOptions.RequiredLength} characters long.");

        if (_passwordOptions.RequireUppercase)
            rule = rule.Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.");

        if (_passwordOptions.RequireLowercase)
            rule = rule.Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.");

        if (_passwordOptions.RequireDigit)
            rule = rule.Matches(@"[0-9]").WithMessage("Password must contain at least one digit.");

        if (_passwordOptions.RequireNonAlphanumeric)
            rule = rule.Matches(@"[\W]").WithMessage("Password must contain at least one special character.");
    }

    private void CheckUrlAttribute(PropertyInfo property, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var urlAttr = property.GetCustomAttributes<UrlAttribute>().FirstOrDefault();
        if (urlAttr == null || property.PropertyType != typeof(string))
            return;
        if (actualType != typeof(string))
            throw new Exception("Url attribute is only applicable to properties of type String.");

        RuleFor(x => (string?)property.GetValue(x)) // Explicitly cast to the property type
            .Matches(@"^(http|https)://.*$") // Basic URL validation
            .WithMessage(urlAttr.Message);
    }

    private void CheckRegularExpressionAttribute(PropertyInfo property, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var regexAttr = property.GetCustomAttributes<RegularExpressionAttribute>().FirstOrDefault();
        if (regexAttr == null || property.PropertyType != typeof(string))
            return;

        if (actualType != typeof(string))
            throw new Exception("RegularExpression attribute is only applicable to properties of type String.");

        RuleFor((Expression<Func<T, string>>)propertyAccessExpr) // Explicitly cast to the property type
            .Matches(regexAttr.Pattern)
            .WithMessage(regexAttr.Message);
    }

    private void CheckCreditCardAttribute(PropertyInfo property, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var creditCardAttr = property.GetCustomAttributes<CreditCardAttribute>().FirstOrDefault();
        if (creditCardAttr == null)
            return;

        if (CodeKit.IsNumericType(actualType))
            throw new Exception("CreditCard attribute is only applicable to properties of type String.");

        RuleFor((Expression<Func<T, string>>)propertyAccessExpr) // Explicitly cast to the property type
            .Matches(@"^4[0-9]{12}(?:[0-9]{3})?$|^5[1-5][0-9]{14}$|^3[47][0-9]{13}$|^3(?:0[0-5]|[68][0-9])[0-9]{11}$|^6(?:011|5[0-9]{2})[0-9]{12}$|^7[0-9]{15}$")
            .WithMessage(creditCardAttr.Message);
    }

    private void CheckCompareAttribute(PropertyInfo property, PropertyInfo[] properties, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var compareAttr = property.GetCustomAttributes<TextCompareAttribute>().FirstOrDefault();
        if (compareAttr == null)
            return;

        if (CodeKit.IsNumericType(actualType))
            throw new Exception("TextCompare attribute is only applicable to properties of type String.");

        var otherProperty = properties.FirstOrDefault(p => p.Name == compareAttr.OtherProperty);
        if (otherProperty != null)
        {
            RuleFor((Expression<Func<T, string>>)propertyAccessExpr) // Explicitly cast to the property type
                .Equal(x => otherProperty.GetValue(x))
                .WithMessage(compareAttr.Message);
        }
    }

    private void CheckRangeAttribute(PropertyInfo property, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var rangeAttr = property.GetCustomAttributes<NumericRangeAttribute>().FirstOrDefault();
        if (rangeAttr == null)
            return;

        if (!CodeKit.IsNumericType(actualType))
            throw new Exception("NumericRange attribute is only applicable to properties of numeric types.");

        // Handle numerical types
        if (property.PropertyType == typeof(int) || Nullable.GetUnderlyingType(property.PropertyType) == typeof(int))
        {
            var typedExpr = Expression.Lambda<Func<T, int>>(
                Expression.Convert(propertyAccessExpr.Body, typeof(int)),
                propertyAccessExpr.Parameters);

            RuleFor(typedExpr)
                .InclusiveBetween(rangeAttr.Min, rangeAttr.Max)
                .WithMessage(rangeAttr.Message);
        }
        else if (property.PropertyType == typeof(double) || Nullable.GetUnderlyingType(property.PropertyType) == typeof(double))
        {
            var typedExpr = Expression.Lambda<Func<T, double>>(
                Expression.Convert(propertyAccessExpr.Body, typeof(double)),
                propertyAccessExpr.Parameters);

            RuleFor(typedExpr)
                .InclusiveBetween(rangeAttr.Min, rangeAttr.Max)
                .WithMessage(rangeAttr.Message);
        }
        else if (property.PropertyType == typeof(decimal) || Nullable.GetUnderlyingType(property.PropertyType) == typeof(decimal))
        {
            var typedExpr = Expression.Lambda<Func<T, decimal>>(
                Expression.Convert(propertyAccessExpr.Body, typeof(decimal)),
                propertyAccessExpr.Parameters);

            RuleFor(typedExpr)
                .InclusiveBetween(rangeAttr.Min, rangeAttr.Max)
                .WithMessage(rangeAttr.Message);
        }
    }

    private void CheckEmailAttribute(PropertyInfo property, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var emailAttr = property.GetCustomAttributes<EmailAttribute>().FirstOrDefault();
        if (emailAttr == null)
            return;

        if (actualType != typeof(string))
            throw new Exception("Email attribute is only applicable to properties of type String.");

        /*RuleFor((Expression<Func<T, string>>)propertyAccessExpr) // Explicitly cast to the property type
            .EmailAddress()
            .WithMessage(emailAttr.Message);*/


        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(parameter, property);
        var convertedProperty = Expression.Convert(propertyAccess, typeof(string)); // Convert to object to handle any type

        // Create the property expression: x => x.Property
        var expression = Expression.Lambda<Func<T, string>>(convertedProperty, parameter);

        // Use the expression directly in RuleFor to preserve PropertyName
        var rule = RuleFor(expression)
                        .EmailAddress()
                        .WithMessage(emailAttr.Message);
    }

    private void CheckMinLengthAttribute(PropertyInfo property, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var minLengthAttr = property.GetCustomAttributes<MinLengthAttribute>().FirstOrDefault();
        if (minLengthAttr == null)
            return;

        if (actualType != typeof(string))
            throw new Exception("MinLength attribute is only applicable to properties of type String.");

        // Ensure that the property is not nullable before applying the Required attribute
        /*if (nullabilityInfo.ReadState == NullabilityState.Nullable ||
            nullabilityInfo.WriteState == NullabilityState.Nullable)
            throw new Exception("Cannot use MinLength attribute on property marked as nullable for read or write.");*/

        /*RuleFor((Expression<Func<T, string>>)propertyAccessExpr) // Explicitly cast to the property type
            .MinimumLength(minLengthAttr.Length)
            .WithMessage(minLengthAttr.Message);*/

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(parameter, property);
        var convertedProperty = Expression.Convert(propertyAccess, typeof(string)); // Convert to object to handle any type

        // Create the property expression: x => x.Property
        var expression = Expression.Lambda<Func<T, string>>(convertedProperty, parameter);

        // Use the expression directly in RuleFor to preserve PropertyName
        var rule = RuleFor(expression)
                        .MinimumLength(minLengthAttr.Length)
                        .WithMessage(minLengthAttr.Message);
    }

    private void CheckMaxLengthAttribute(PropertyInfo property, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var maxLengthAttr = property.GetCustomAttributes<MaxLengthAttribute>().FirstOrDefault();
        if (maxLengthAttr == null)
            return;

        if (actualType != typeof(string))
            throw new Exception("MaxLength attribute is only applicable to properties of type String.");

        // Ensure that the property is not nullable before applying the Required attribute
        /*if (nullabilityInfo.ReadState == NullabilityState.Nullable ||
            nullabilityInfo.WriteState == NullabilityState.Nullable)
            throw new Exception("Cannot use MaxLength attribute on property marked as nullable for read or write.");*/

        /*RuleFor((Expression<Func<T, string>>)propertyAccessExpr) // Explicitly cast to the property type
            .MaximumLength(maxLengthAttr.Length)
            .WithMessage(maxLengthAttr.Message);*/


        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(parameter, property);
        var convertedProperty = Expression.Convert(propertyAccess, typeof(string)); // Convert to object to handle any type

        // Create the property expression: x => x.Property
        var expression = Expression.Lambda<Func<T, string>>(convertedProperty, parameter);

        // Use the expression directly in RuleFor to preserve PropertyName
        var rule = RuleFor(expression)
                        .MaximumLength(maxLengthAttr.Length)
                        .WithMessage(maxLengthAttr.Message);

    }

    private void CheckRequiredAttribute(PropertyInfo property, LambdaExpression propertyAccessExpr, Type actualType)
    {
        var requiredAttr = property.GetCustomAttributes<RequiredAttribute>().FirstOrDefault();
        if (requiredAttr == null)
            return;

        // Ensure that the property is not nullable before applying the Required attribute
        /*if (nullabilityInfo.ReadState == NullabilityState.Nullable ||
            nullabilityInfo.WriteState == NullabilityState.Nullable)
            throw new Exception("Require attribute is only applicable on a property not marked as nullable for read or write.");*/

        /*RuleFor(x => propertyAccessExpr.Compile().DynamicInvoke(x)) // Explicitly cast to the property type
            .NotEmpty()
            .WithMessage(requiredAttr.Message);*/

        // Explicitly cast to the property type. This should be a property-specific expression
        /*RuleFor((Expression<Func<T, string>>)propertyAccessExpr)
            .NotEmpty()
            .WithMessage(requiredAttr.Message);*/

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(parameter, property);
        var convertedProperty = Expression.Convert(propertyAccess, typeof(object)); // Convert to object to handle any type

        // Create the property expression: x => x.Property
        var expression = Expression.Lambda<Func<T, object>>(convertedProperty, parameter);

        // Use the expression directly in RuleFor to preserve PropertyName
        var rule = RuleFor(expression)
                        .NotEmpty()
                        .WithMessage(requiredAttr.Message);
    }


    /*private static LambdaExpression CreatePropertyExpression<T>(PropertyInfo propertyInfo, Type actualType)
    {
        ParameterExpression param = Expression.Parameter(typeof(T), "x");
        MemberExpression property = Expression.Property(param, propertyInfo);
        UnaryExpression convertedProperty = Expression.Convert(property, actualType); // Ensure the type matches

        // Create a lambda expression with dynamic return type
        return Expression.Lambda(convertedProperty, property.Member.Name, [param]);
    }*/

    // You need to implement CreatePropertyExpression<T> to generate the property access expression
    private Expression<Func<T, object>> CreatePropertyExpression<T>(PropertyInfo property, Type actualType)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(parameter, property);

        // If the property is a value type, box it to object
        var castedPropertyAccess = Expression.Convert(propertyAccess, typeof(object));

        return Expression.Lambda<Func<T, object>>(castedPropertyAccess, parameter);
    }

    private LambdaExpression CreateTypedLambda<T>(LambdaExpression propertyAccessExpr, Type actualType)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Invoke(propertyAccessExpr, parameter);
        var convertedProperty = Expression.Convert(propertyAccess, actualType);

        return Expression.Lambda(convertedProperty, parameter);
    }

    private void ApplyRequiredValidation(PropertyInfo property, Type actualType, string errorMessage)
    {
        // Create the lambda expression (x => x.Property)
        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(parameter, property);
        var lambda = Expression.Lambda(propertyAccess, parameter);

        // Find RuleFor method using reflection
        var ruleForMethod = typeof(AbstractValidator<T>)
            .GetMethods()
            .FirstOrDefault(m => m.Name == "RuleFor"
                && m.IsGenericMethod
                && m.GetParameters().First().ParameterType.GetGenericTypeDefinition() == typeof(Expression<>));

        if (ruleForMethod == null)
        {
            throw new InvalidOperationException($"Could not find RuleFor method for type {actualType}.");
        }

        // Make RuleFor generic based on the property type (actualType)
        var genericRuleFor = ruleForMethod.MakeGenericMethod(actualType);

        // Invoke the RuleFor method
        var rule = genericRuleFor.Invoke(this, new object[] { lambda });

        // Find and invoke NotEmpty method dynamically if it's valid for the property type
        var notEmptyMethod = GetMethodFromTypeOrInterfaces(rule, "NotEmpty");
        if (notEmptyMethod != null)
        {
            notEmptyMethod.Invoke(rule, null);

            // Set the custom error message
            var withMessageMethod = rule.GetType().GetMethod("WithMessage", new[] { typeof(string) });
            withMessageMethod.Invoke(rule, new object[] { errorMessage });
        }
        else
        {
            // Optionally handle non-applicable types, such as non-nullable value types
            // For non-string types, you can add other validation rules here if needed
            Console.WriteLine($"Property {property.Name} is not a string or a type that supports NotEmpty.");
        }
    }

    private MethodInfo GetMethodFromTypeOrInterfaces(object instance, string methodName)
    {
        var type = instance.GetType();

        // Check type first
        var method = type.GetMethod(methodName);
        if (method != null)
        {
            return method;
        }

        // Then check the interfaces implemented by the type
        foreach (var iface in type.GetInterfaces())
        {
            method = iface.GetMethod(methodName);
            if (method != null)
            {
                return method;
            }
        }

        return null;
    }


}
