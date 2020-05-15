namespace YouYiApp.model
{
    public class Player
    {

        public static Player play = null;

        private static object singleton_Lock = new object(); //锁同步

        public Player()
        {
            this.weapon0 = new Weapon();
            this.weapon1 = new Weapon();
            this.useWeapon = this.weapon0;
            this.runXueWuLogic = false;
            this.position = "站";
            this.useMissiles = false;
            this.XDown = false;
            this.ESCDown = false;
            this.runLogic = true;
        }

        // 武器1
        public Weapon weapon0 { get; set; }

        // 武器2
        public Weapon weapon1 { get; set; }

        // 当前使用武器
        public Weapon useWeapon { get; set; }

        // 运行血雾逻辑
        public bool runXueWuLogic { get; set; }

        // 是否运行压枪逻辑
        public bool runLogic { get; set; }

        // X 按键状态
        public bool XDown { get; set; }

        // ESC 按键状态
        public bool ESCDown { get; set; }

        // 使用瞄具
        public bool useScope { get; set; }

        // 使用投掷物
        public bool useMissiles { get; set; }

        // 姿势 站 蹲 趴
        public string position { get; set; }

        public bool isShoot { get; set; }

        // 自动射击
        public bool autoShoot { get; set; }

        // 是否打开背包
        public bool openBag { get; set; }
        
        public static Player GetPlay()
        {
            lock (singleton_Lock)
            {
                if (null == play)
                {
                    play = new Player();
                }
            }
            return play;
        }

    }
}