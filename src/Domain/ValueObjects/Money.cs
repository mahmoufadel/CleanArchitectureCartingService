namespace CleanArchitecture.Domain.ValueObjects;


    public class Money : IEquatable<Money>
    {
        public Money(string currency, decimal amount)
        {
            this.Currency = currency;
            this.Amount = amount;
        }

        public string Currency { get; private set; }
        public decimal Amount { get; private set; }

        public bool Equals(Money other)
        {
            if (object.ReferenceEquals(other, null)) return false;
            if (object.ReferenceEquals(other, this)) return true;
            return this.Currency.Equals(other.Currency) && this.Amount.Equals(other.Amount);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public override int GetHashCode()
        {
            return this.Currency.GetHashCode() ^ this.Amount.GetHashCode();
        }
    }

