using Rage;
using static Rage.Native.NativeFunction;

namespace BlueLightSoftware.Common.Extensions
{
    public static class BlipExtensions
    {
        /// <summary>
        /// Removes and deletes the <see cref="Blip"/> properly since
        /// <see cref="Blip.Delete"/> currently does not work.
        /// </summary>
        /// <param name="blip"></param>
        public static void Remove(this Blip blip)
        {
            Natives.RemoveBlip(ref blip);
        }
    }
}
