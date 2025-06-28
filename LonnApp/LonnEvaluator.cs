namespace LonnApp
{
    public class LonnEvaluator
    {
        // الثوابت لتحديد الشروط
        private const double MinIncome = 2000;
        private const double MaxLoanAmount = 1000;
        private const int MaxLoanTerm = 10;

        // الدالة الرئيسية - التعقيد الحلقي = 1
        public string GetLonnEligibility(double income, bool isEmployed, double loanAmount, int loanTerm, bool hasGoodCredit)
        {
            // تحقق من صحة المدخلات
            if (income < 0 || loanAmount < 0 || loanTerm < 0)
                return "Invalid Input";
            
            // استدعاء الدالة المساعدة
            return IsEligible(income, isEmployed, loanAmount, loanTerm, hasGoodCredit) 
                ? "Eligible" 
                : "Not Eligible";
        }

        // دالة مساعدة للتحقق الشامل - التعقيد الحلقي = 5
        private bool IsEligible(double income, bool isEmployed, double loanAmount, int loanTerm, bool hasGoodCredit)
        {
            return IsIncomeValid(income) &&
                   IsEmploymentValid(isEmployed) &&
                   IsLoanAmountValid(loanAmount) &&
                   IsLoanTermValid(loanTerm) &&
                   IsCreditValid(hasGoodCredit);
        }

        // الدوال الفرعية - كل منها له تعقيد حلقي = 1
        public bool IsIncomeValid(double income) => income >= MinIncome;
        public bool IsEmploymentValid(bool isEmployed) => isEmployed;
        public bool IsLoanAmountValid(double loanAmount) => loanAmount <= MaxLoanAmount;
        public bool IsLoanTermValid(int loanTerm) => loanTerm <= MaxLoanTerm;
        public bool IsCreditValid(bool hasGoodCredit) => hasGoodCredit;
    }
}