using Kalendra.Itemite.Runtime.Domain;

namespace Kalendra.Itemite.Tests
{
    internal static class TestApi
    {
        public static Item Tree => new Item("Tree", "Plant, Vegetable, Natural, Raw materal, Wooden");
        public static Item Fire => new Item("Fire", "Natural, Flammable");
        public static Item Apple => new Item("Apple", "Natural, Fruit, Vegetable, Food");
        public static Item Paper => new Item("Paper", "Raw material, Flammable, Raw material");
        public static Item Car => new Item("Car", "Vehicle, Transport, Tech, Flammable");
        public static Item House => new Item("House", "Building, Home, Tech, Wooden");
        public static Item Table => new Item("Table", "Tech, Wooden, Furniture, Home");
        public static Item[] SomeItems => new[] { Tree, Fire, Apple, Paper, Car, House, Table };
    }
}