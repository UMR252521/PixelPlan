using System.Collections.Generic;

namespace AdjustNS
{
    public interface AdjustClientListener
    {
        /// <summary>
        /// redeem the switch
        /// </summary>
        /// <param name="isOpen">true is open either or not</param>
        void onStuffTurnChanged(bool isOpen);
        void versionConfig(string version);
    }
}