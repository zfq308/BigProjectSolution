using NUnit.Framework;
using BigProject.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigProject.CommonModel;

namespace BigProject.Utils.Tests
{
    [TestFixture()]
    public class EnumTypeHelperTests
    {
        [Test()]
        public void GetEnumByIdTest()
        {
            var a = EnumTypeHelper.GetEnumById<MessageCode>(100);
            if(a!=MessageCode.OK)
            {
                Assert.Fail();
            }
        }

        [Test()]
        public void GetEnumByNameTest()
        {
            var a = EnumTypeHelper.GetEnumByName<MessageCode>("OK");
            if (a != MessageCode.OK)
            {
                Assert.Fail();
            }
        }
    }
}