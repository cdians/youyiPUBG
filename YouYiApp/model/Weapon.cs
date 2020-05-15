using Newtonsoft.Json.Linq;

namespace YouYiApp.model
{
    public class Weapon
    {

        // 武器
        public string weapon { get; set; } = "空";

        // 瞄具
        public string scope { get; set; } = WeaponList.SCOPE1X_HD;

        // 枪口
        public string qiangkou { get; set; } = "枪口空";

        // 握把
        public string woBa { get; set; } = "握把空";

        // 弹匣
        public string magazine { get; set; } = "弹匣空";

        // 枪托
        public string qiangTuo { get; set; } = "枪托空";

        // 压枪数据
        public SqlDataModel modelData { get; set; }

        public void ClearAttr()
        {
            this.weapon = "空";
            this.scope = WeaponList.SCOPE1X_HD;
            this.qiangkou = "枪口空";
            this.woBa = "握把空";
            this.magazine = "弹匣空";
            this.qiangTuo = "枪托空";
            this.modelData = null;
        }

    }
}