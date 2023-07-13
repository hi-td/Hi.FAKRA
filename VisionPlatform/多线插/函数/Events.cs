
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionPlatform
{
    public delegate void ControlChangedEventHandler(int count);
    //public delegate void PanelChangedEventHandler(int id, InspectType type);
    public delegate void ValueChangedEventHandler();
    public delegate void FitCircleValueChangedEventHandler(object sender, EventArgs e);
    public delegate void LocationSetValueChangedEvenHandler(object sender, EventArgs e);
    public static class MyEvents
    {
        public static event ControlChangedEventHandler RubberCountChanged;
        //public static event PanelChangedEventHandler PinCountChanged;

        public static void RubberChanged(this int count) => RubberCountChanged?.Invoke(count);

        //public static void PinChanged(this int id, InspectType type) => PinCountChanged?.Invoke(id, type);

        public static event ValueChangedEventHandler ValueChanged;
        public static void ValueChange() => ValueChanged?.Invoke();
        //插壳检测
        //public static event FitCircleValueChangedEventHandler FitCircleValueChanged;
        //public static void FitCircleValueChange(this object sender, EventArgs e) => FitCircleValueChanged?.Invoke(sender, e);
       
    }
}
