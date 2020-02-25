using SecuritySystemListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Raw> Raws { get; set; }
        public List<Order> Orders { get; set; }
        public List<Equipment> Equipments { get; set; }
        public List<EquipmentRaw> EquipmentRaws { get; set; }

        private DataListSingleton()
        {
            Raws = new List<Raw>();
            Orders = new List<Order>();
            Equipments = new List<Equipment>();
            EquipmentRaws = new List<EquipmentRaw>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
