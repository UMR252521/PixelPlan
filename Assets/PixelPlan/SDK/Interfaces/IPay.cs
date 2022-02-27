namespace pixelplan
{
    public interface IPay
    {
        void DoPay(string scene, string product);
        void DoSub(string scene, string product);
        bool SubsAvailable();
    }
}
