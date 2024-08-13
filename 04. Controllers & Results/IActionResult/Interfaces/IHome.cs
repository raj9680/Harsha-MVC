namespace IActionResult.Interfaces
{
    public abstract class Car
    {
        public int AccountNumber(int id)
        {
            return id;
        }

        public abstract string Credit();
        public abstract string Debit();
        public abstract string Trasact();
    }
}
