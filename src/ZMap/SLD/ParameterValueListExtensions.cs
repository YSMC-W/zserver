using System.Linq;
using ZMap.Infrastructure;
using ZMap.SLD.Filter.Expression;
using ZMap.Style;

namespace ZMap.SLD;

public static class ParameterValueListExtensions
{
    public static ParameterValue GetOrDefault(this NamedParameter[] parameters, string name)
    {
        return parameters.FirstOrDefault(x => x.Name == name);
    }

    public static CSharpExpression<T> BuildExpression<T>(this ParameterValue parameter, IStyleVisitor visitor,
        object extraData,
        T defaultValue)
    {
        if (parameter == null)
        {
            return CSharpExpression<T>.New(defaultValue);
        }
        else
        {
            parameter.Accept(visitor, extraData);
            var value = visitor.Pop();
            return value == null ? CSharpExpression<T>.New(defaultValue) : CSharpExpression<T>.From(value, defaultValue);
        }
    }

    public static CSharpExpression<T[]> BuildArrayExpression<T>(this ParameterValue parameter, IExpressionVisitor visitor,
        object extraData,
        T[] defaultValue)
    {
        if (parameter == null)
        {
            return CSharpExpression<T[]>.New(defaultValue);
        }
        else
        {
            var text = parameter.Text?.ElementAtOrDefault(0);
            if (text == null)
            {
                foreach (var expressionType in parameter.Items)
                {
                    expressionType.Accept(visitor, extraData);
                }

                var result = visitor.Pop();
                if (result is CSharpExpression expression)
                {
                    return result as CSharpExpression<T[]> ?? CSharpExpression<T[]>.New(null, expression.Body);
                }
                else
                {
                    text = result.ToString();
                }
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return CSharpExpression<T[]>.New(defaultValue);
            }
            else
            {
                var array = ConvertUtilities.ToArray<T>(text);
                return CSharpExpression<T[]>.New(array);
            }
        }
    }
}