namespace YouYiApp.model
{
    public class WeaponDataModel
    {

        public WeaponDataModel(double num, int index)
        {
            this.Num = num;
            this.Index = index;
        }

        public double Num { get; set; }

        public int Index { get; set; }
    }
}
