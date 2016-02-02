using System;

namespace BigProject.Utils
{
    public class EnumTypeHelper
    {
        public static EnumType GetEnumById<EnumType>(int id)
        {
            EnumType type = default (EnumType);
            try
            {
                if (Enum.IsDefined(typeof (EnumType), id))
                {
                    type = (EnumType)Enum.Parse(typeof (EnumType), id.ToString());
                }
            }
            catch
            {
                return type;
            }

            return type;
        }

        public static EnumType GetEnumByName<EnumType>(string name)
        {
            EnumType type = default (EnumType);
            try
            {
                if (Enum.IsDefined(typeof (EnumType), name))
                {
                    type = (EnumType)Enum.Parse(typeof (EnumType), name);
                }
            }
            catch
            {
                return type;
            }

            return type;
        }
    }
}