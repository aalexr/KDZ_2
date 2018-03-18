using System;
using System.Windows.Forms;

partial class Form1
{
}
class Program
{
    [STAThread()]
    public static void Main() => Application.Run(new Form1());
}
