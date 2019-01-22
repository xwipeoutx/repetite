namespace Repetite
{
    public interface IBehaviour
    {
        Input[] Inputs { get; }
        Output[] Outputs { get; }
        IValueBag Execute(IValueBag inputs);
    }
}