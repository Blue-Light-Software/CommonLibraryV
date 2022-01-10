using Rage;
using System;
using System.Linq;
using static Rage.Native.NativeFunction;

namespace BlueLightSoftware.Common.Extensions
{
    /// <summary>
    /// Credits to Albo1125 and the scripthookvdotnet team for most of these methods
    /// </summary>
    /// <seealso cref="https://github.com/crosire/scripthookvdotnet/blob/main/source/scripting_v3/GTA/World.cs"/>
    /// <seealso cref="https://github.com/Albo1125/Albo1125-Common/blob/master/Albo1125.Common/CommonLibrary/SpawnPoint.cs"/>
    /// <seealso cref="https://gtaforums.com/topic/843561-pathfind-node-types/"/>
    /// <seealso cref="https://gta.fandom.com/wiki/Paths_(GTA_V)"/>
    public static class Vector3Extensions
    {
        public static int[] BlackListedNodeTypes = new int[] { 0, 8, 9, 10, 12, 40, 42, 136 };

        /// <summary>
        /// Gets the closest in game vehicle node to this <see cref="Vector3"/> position
        /// </summary>
        /// <param name="pos">The position to check around</param>
        /// <returns></returns>
        public static Vector3 GetClosestMajorVehicleNode(this Vector3 pos)
        {
            Natives.GET_CLOSEST_MAJOR_VEHICLE_NODE<bool>(pos.X, pos.Y, pos.Z, out Vector3 node, 3.0f, 0f);
            return node;
        }

        public static int GetNearestNodeType(this Vector3 pos)
        {
            if (Natives.GetVehicleNodeProperties<bool>(pos.X, pos.Y, pos.Z, out uint density, out int nodeType))
            {
                return nodeType;
            }
            else
            {
                return -1;
            }
        }

        public static bool IsNodeSafe(this Vector3 pos)
        {
            return !BlackListedNodeTypes.Contains(GetNearestNodeType(pos));
        }

        /// <summary>
        /// Returns wether this <see cref="Vector3"/> position us on water
        /// </summary>
        /// <param name="pos">The position to check</param>
        /// <returns></returns>
        public static bool IsPointOnWater(this Vector3 pos)
        {
            return Natives.GetWaterHeight<bool>(pos.X, pos.Y, pos.Z, out float height);
        }

        /// <summary>
        /// Calculates and returns the heading between this position and the specified
        /// <see cref="Vector3"/> position
        /// </summary>
        /// <param name="start">The position to start from</param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static float CalculateHeadingTowardsPosition(this Vector3 start, Vector3 position)
        {
            Vector3 directionToTargetEnt = (position - start);
            directionToTargetEnt.Normalize();
            return MathHelper.ConvertDirectionToHeading(directionToTargetEnt);
        }

        /// <summary>
        /// Converts the comma-seperated string representation of a vector3 to its <see cref="Vector3"/> equivalent.
        /// </summary>
        /// <param name="v">A string containing the values to convert.</param>
        /// <param name="vector"></param>
        /// <returns>true if <paramref name="v"/> was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string v, out Vector3 vector)
        {
            // Ensure our string is not empty
            if (String.IsNullOrEmpty(v))
            {
                vector = Vector3.Zero;
                return false;
            }

            // Ensure we have 3 vector coordinates
            string[] parts = v.Split(',');
            if (parts.Length != 3)
            {
                vector = Vector3.Zero;
                return false;
            }

            // Parse each coordinate from string to float
            float[] coords = new float[3] { 0f, 0f, 0f };
            for (int i = 0; i < 3; i++)
            {
                // try and parse string value
                if (!float.TryParse(parts[i].Trim(), out float val))
                {
                    vector = Vector3.Zero;
                    return false;
                }
                
                coords[i] = val;
            }

            // If we are here, we parsed good
            vector = new Vector3(coords[0], coords[1], coords[2]);
            return true;
        }
    }
}
