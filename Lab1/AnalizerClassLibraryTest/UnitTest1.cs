using AnalaizerClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace AnalizerClassLibraryTest
{
    [TestClass]
    public class AnalizerClassLibraryTestClass
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("System.Data.SqlClient", "Data Source=DESKTOP-123023I;Initial Catalog=TestDatabase;Integrated Security=True;", "TestExpressionsData", DataAccessMethod.Sequential)]
        public void TestCreateStackWithExpressions()
        {
            string expression = TestContext.DataRow["Expression"].ToString();
            string expectedStr = TestContext.DataRow["ExpectedResult"].ToString();
            ArrayList expected = new ArrayList(expectedStr.Split(','));

            AnalaizerClass.expression = expression;
            ArrayList result = AnalaizerClass.CreateStack();

            CollectionAssert.AreEqual(expected, result);
        }
    }
}