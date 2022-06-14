using Microsoft.VisualStudio.TestTools.UnitTesting;
using Peter_image_sorter;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        DateObject expected;

        [TestInitialize]
        public void TestInitialise()
        {
            expected = new();
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_s21Heic()
        {
            // setup
            string fileName = "20211114_190938.heic";

            expected.Day = "14";
            expected.Month = "11";
            expected.Year = "2021";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            DatesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_WhatsApp()
        {
            // setup
            string fileName = "VID-20211112-WA0008.mp4";
            
            expected.Day = "12";
            expected.Month = "11";
            expected.Year = "2021";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            DatesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_OnePlus9ProHeic()
        {
            // setup
            string fileName = "IMG20220122112747.heic";
            
            expected.Day = "22";
            expected.Month = "01";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            DatesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_EufyVideo()
        {
            // setup
            string fileName = "_2022_06_08_19_46_08.mp4";

            
            expected.Day = "08";
            expected.Month = "06";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            DatesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_OnePlus9ProVideo()
        {
            // setup
            string fileName = "VID_20220103_205223.mp4";

            
            expected.Day = "03";
            expected.Month = "01";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            DatesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_S21Video()
        {
            // setup
            string fileName = "20220102_191014.mp4";

            
            expected.Day = "02";
            expected.Month = "01";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            DatesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_OnePlus9ProVideo_2()
        {
            // setup
            string fileName = "VID20220126210857.mp4";

            
            expected.Day = "26";
            expected.Month = "01";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            DatesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_EufyFromDevice_PeterCamera()
        {
            // setup
            string fileName = "_PeterCamera_2022_05_15_09_26_49.mp4";

            
            expected.Day = "15";
            expected.Month = "05";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            DatesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_EufyFromDevice_BackYard()
        {
            // setup
            string fileName = "_Backyard_2022_05_11_16_14_01.mp4";

            
            expected.Day = "11";
            expected.Month = "05";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            DatesMatch(expected, actual);
        }
        private static void DatesMatch(DateObject expected, DateObject actual)
        {
            // assert that the day, month and year match what they should be
            Assert.AreEqual(expected.Day, actual.Day);
            Assert.AreEqual(expected.Month, actual.Month);
            Assert.AreEqual(expected.Year, actual.Year);
        }

    }
}
