namespace GEMC
{
    public abstract class LetterDecorator : Letter
    {
        protected Letter letterItem;

        public LetterDecorator(Letter letterItem)
        {
            this.letterItem = letterItem;
        }
    }
}
