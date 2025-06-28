using Xunit;
using LonnApp;

namespace LonnApp.Tests
{
    public class LonnEvaluatorTests
    {
        // === اختبارات الدوال الفرعية ===
        [Theory]
        [InlineData(1999, false)]
        [InlineData(2000, true)]
        [InlineData(2500, true)]
        public void IsIncomeValid_Returns_Correctly(double income, bool expected)
        {
            var evaluator = new LonnEvaluator();
            Assert.Equal(expected, evaluator.IsIncomeValid(income));
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void IsEmploymentValid_Returns_Correctly(bool isEmployed, bool expected)
        {
            var evaluator = new LonnEvaluator();
            Assert.Equal(expected, evaluator.IsEmploymentValid(isEmployed));
        }

        [Theory]
        [InlineData(1001, false)]
        [InlineData(1000, true)]
        [InlineData(999, true)]
        public void IsLoanAmountValid_Returns_Correctly(double loanAmount, bool expected)
        {
            var evaluator = new LonnEvaluator();
            Assert.Equal(expected, evaluator.IsLoanAmountValid(loanAmount));
        }

        [Theory]
        [InlineData(11, false)]
        [InlineData(10, true)]
        [InlineData(9, true)]
        public void IsLoanTermValid_Returns_Correctly(int loanTerm, bool expected)
        {
            var evaluator = new LonnEvaluator();
            Assert.Equal(expected, evaluator.IsLoanTermValid(loanTerm));
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void IsCreditValid_Returns_Correctly(bool hasGoodCredit, bool expected)
        {
            var evaluator = new LonnEvaluator();
            Assert.Equal(expected, evaluator.IsCreditValid(hasGoodCredit));
        }

        // === اختبارات الدالة الرئيسية ===
        [Theory]
        [InlineData(1500, true, 800, 5, true, "Not Eligible")] // دخل غير صالح
        [InlineData(2500, false, 800, 5, true, "Not Eligible")] // غير موظف
        [InlineData(2500, true, 1200, 5, true, "Not Eligible")] // مبلغ قرض كبير
        [InlineData(2500, true, 800, 11, true, "Not Eligible")] // مدة قرض طويلة
        [InlineData(2500, true, 800, 5, false, "Not Eligible")] // ائتمان سيء
        [InlineData(2500, true, 800, 5, true, "Eligible")] // كل الشروط صحيحة
        [InlineData(-100, true, 800, 5, true, "Invalid Input")] // دخل سلبي
        [InlineData(2500, true, -500, 5, true, "Invalid Input")] // مبلغ قرض سلبي
        [InlineData(2500, true, 800, -2, true, "Invalid Input")] // مدة قرض سلبية
        public void GetLonnEligibility_Returns_Correctly(
            double income, bool isEmployed, double loanAmount, 
            int loanTerm, bool hasGoodCredit, string expected)
        {
            var evaluator = new LonnEvaluator();
            var result = evaluator.GetLonnEligibility(income, isEmployed, loanAmount, loanTerm, hasGoodCredit);
            Assert.Equal(expected, result);
        }

        // === اختبارات التكامل ===
        [Fact]
        public void FullIntegrationTest_ValidInput_ReturnsEligible()
        {
            var evaluator = new LonnEvaluator();
            var result = evaluator.GetLonnEligibility(2500, true, 800, 5, true);
            Assert.Equal("Eligible", result);
        }

        [Fact]
        public void FullIntegrationTest_InvalidIncome_ReturnsNotEligible()
        {
            var evaluator = new LonnEvaluator();
            var result = evaluator.GetLonnEligibility(1500, true, 800, 5, true);
            Assert.Equal("Not Eligible", result);
        }
        
        [Fact]
        public void FullIntegrationTest_InvalidInput_ReturnsError()
        {
            var evaluator = new LonnEvaluator();
            var result = evaluator.GetLonnEligibility(-100, true, 800, 5, true);
            Assert.Equal("Invalid Input", result);
        }
    }
}