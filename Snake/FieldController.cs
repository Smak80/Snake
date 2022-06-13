using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake;
public class FieldController
{
    private Field _field;
    private Thread? t;
    private int _pause = 2000;
    public FieldController(Field f)
    {
        _field = f;
    }

    public void Start()
    {
        if (t == null || !t.IsAlive)
        {
            t = new Thread(Go);
            t.IsBackground = true;
            t.Start();
        }
    }

    public void Go()
    {
        while (true)
        {
            Thread.Sleep(_pause);
            _pause -= 10;
            _field.NextStep();
        }
    }
}
