namespace Lab6_TheBar
{
    internal class Bartender
    {
        Patron currentPatron;
        Glass currentGlass;
        Bar bar;

        public Bartender(Bar bar)
        {
            this.bar = bar;
        }
    }
}