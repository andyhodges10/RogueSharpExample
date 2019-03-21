using RogueSharpExample.Equipment;

namespace RogueSharpExample.Systems
{
    public class EquipmentGenerator
    {
        private readonly Pool<Core.Equipment> _equipmentPool;

        public EquipmentGenerator(int level)
        {
            _equipmentPool = new Pool<Core.Equipment>();

            if (level <= 3)
            {
                _equipmentPool.Add(BodyEquipment.Leather(),   20);
                _equipmentPool.Add(HeadEquipment.Leather(),   20);
                _equipmentPool.Add(FeetEquipment.Leather(),   20);
                _equipmentPool.Add(HandEquipment.Dagger(),    23); // 83

                _equipmentPool.Add(HeadEquipment.Chain(),      2);
                _equipmentPool.Add(BodyEquipment.Chain(),      2);
                _equipmentPool.Add(FeetEquipment.Chain(),      2);
                _equipmentPool.Add(HandEquipment.Sword(),      7); // 13

                _equipmentPool.Add(HeadEquipment.Chintin(),    1);
                _equipmentPool.Add(BodyEquipment.Chintin(),    1);
                _equipmentPool.Add(FeetEquipment.Chintin(),    1);
                _equipmentPool.Add(HandEquipment.Axe(),        1); // 4
            }
            else if (level <= 6)
            {
                _equipmentPool.Add(BodyEquipment.Leather(),    6);
                _equipmentPool.Add(HeadEquipment.Leather(),    6);
                _equipmentPool.Add(FeetEquipment.Leather(),    6);
                _equipmentPool.Add(HandEquipment.Dagger(),     9); // 27

                _equipmentPool.Add(BodyEquipment.Chain(),     12);
                _equipmentPool.Add(HeadEquipment.Chain(),     12);
                _equipmentPool.Add(FeetEquipment.Chain(),     12);
                _equipmentPool.Add(HandEquipment.Sword(),     12); // 48

                _equipmentPool.Add(HeadEquipment.Chintin(),    4);
                _equipmentPool.Add(BodyEquipment.Chintin(),    4);
                _equipmentPool.Add(FeetEquipment.Chintin(),    4);
                _equipmentPool.Add(HandEquipment.Axe(),        9); // 21

                _equipmentPool.Add(HeadEquipment.Scaled(),     1);
                _equipmentPool.Add(BodyEquipment.Scaled(),     1);
                _equipmentPool.Add(FeetEquipment.Scaled(),     1);
                _equipmentPool.Add(HandEquipment.TwoHanded(),  1); //  4
            }
            else if (level <= 8)
            {
                _equipmentPool.Add(BodyEquipment.Chintin(),   10);
                _equipmentPool.Add(HeadEquipment.Chintin(),   10);
                _equipmentPool.Add(FeetEquipment.Chintin(),   10); 
                _equipmentPool.Add(HandEquipment.Axe(),       11); // 41

                _equipmentPool.Add(HeadEquipment.Scaled(),    12);
                _equipmentPool.Add(BodyEquipment.Scaled(),    12);
                _equipmentPool.Add(FeetEquipment.Scaled(),    12);
                _equipmentPool.Add(HandEquipment.TwoHanded(), 12); // 48

                _equipmentPool.Add(HeadEquipment.Plate(),      5);
                _equipmentPool.Add(BodyEquipment.Plate(),      5);
                _equipmentPool.Add(FeetEquipment.Plate(),      5);
                _equipmentPool.Add(HandEquipment.Dragonbane(), 6); // 21
            }
            else
            {
                _equipmentPool.Add(HeadEquipment.Scaled(),     8);
                _equipmentPool.Add(BodyEquipment.Scaled(),     8);
                _equipmentPool.Add(FeetEquipment.Scaled(),     8); // 24

                _equipmentPool.Add(HeadEquipment.Plate(),     12); 
                _equipmentPool.Add(BodyEquipment.Plate(),     12);
                _equipmentPool.Add(FeetEquipment.Plate(),     12); 
                _equipmentPool.Add(HandEquipment.TwoHanded(), 10); // 46

                _equipmentPool.Add(HeadEquipment.Mithril(),    6);
                _equipmentPool.Add(BodyEquipment.Mithril(),    6);
                _equipmentPool.Add(FeetEquipment.Mithril(),    6);
                _equipmentPool.Add(HandEquipment.Dragonbane(), 8); // 26

                _equipmentPool.Add(HeadEquipment.Mithril(),    1);
                _equipmentPool.Add(BodyEquipment.Mithril(),    1);
                _equipmentPool.Add(FeetEquipment.Mithril(),    1);
                _equipmentPool.Add(HandEquipment.DragonLord(),  1); //  4
            }
        }

        public Core.Equipment CreateEquipment()
        {
            return _equipmentPool.Get();
        }
    }
}
