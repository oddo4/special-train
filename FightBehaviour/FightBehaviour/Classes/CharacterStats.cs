using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightBehaviour.Classes
{
    class CharacterStats
    {
        public int HP { get; set; }
        public int MP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int MAG_ATK { get; set; }
        public int MAG_DEF { get; set; }
        public int SPD { get; set; }
        public int FIR_RES { get; set; }
        public int ICE_RES { get; set; }
        public int TND_RES { get; set; }
        public int WAT_RES { get; set; }

        public CharacterStats(int hp = 0, int mp = 0, int atk = 0, int def = 0, int mag_atk = 0, int mag_def = 0, int spd = 0, int fir_res = 0, int ice_res = 0, int tnd_res = 0, int wat_res = 0)
        {
            HP = hp;
            MP = mp;
            ATK = atk;
            DEF = def;
            MAG_ATK = mag_atk;
            MAG_DEF = mag_def;
            SPD = spd;
            FIR_RES = fir_res;
            ICE_RES = ice_res;
            TND_RES = tnd_res;
            WAT_RES = wat_res;
        }
    }
}
