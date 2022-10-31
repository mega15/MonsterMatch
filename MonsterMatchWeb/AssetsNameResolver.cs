using Model;

namespace MonsterMatchWeb
{
    public static class AssetsNameResolver
    {
        public static string GetImageName(ElementType elementType)
        {
            string result = null;

            switch (elementType)
            {
                case ElementType.Air:
                    result = "air.png";
                    break;
                case ElementType.Fire:
                    result = "fire.png";
                    break;
                case ElementType.Electric:
                    result = "electric.png";
                    break;
                case ElementType.Water:
                    result = "water.png";
                    break;
                case ElementType.Earth:
                    result = "earth.png";
                    break;
            }

            return result;
        }
    }
}
