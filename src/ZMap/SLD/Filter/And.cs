using System.Xml.Serialization;

namespace ZMap.SLD.Filter;

/// <remarks/>
[System.SerializableAttribute]
[XmlType]
[XmlRoot("And")]
public class And : BinaryLogicOpType
{
    public override object Accept(IFilterVisitor visitor, object extraData)
    {
        var left = Items[0];
        var right = Items[1];
        visitor.VisitObject(left, extraData);
        var leftExpression = (ZMap.Style.CSharpExpression)visitor.Pop();
        visitor.VisitObject(right, extraData);
        var rightExpression = (ZMap.Style.CSharpExpression)visitor.Pop();
        visitor.Push(ZMap.Style.CSharpExpression.New($"{leftExpression.Body} && {rightExpression.Body}"));
        return null;
    }
}