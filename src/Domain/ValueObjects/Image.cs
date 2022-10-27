namespace CleanArchitecture.Domain.ValueObjects;


    public class Image : IEquatable<Image>
    {
        public Image(string altText, string url)
        {
            this.AltText = altText;
            this.Url = url;
        }

        public string AltText { get; private set; }
        public string Url { get; private set; }

        public bool Equals(Image other)
        {
            if (object.ReferenceEquals(other, null)) return false;
            if (object.ReferenceEquals(other, this)) return true;
            return this.AltText.Equals(other.AltText) && this.Url.Equals(other.Url);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Image);
        }

        public override int GetHashCode()
        {
            return this.Url.GetHashCode() ^ this.AltText.GetHashCode();
        }
    }

