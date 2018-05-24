using System;
using System.Collections.Generic;

namespace JobFair.Tagit.Sgtin
{
    static class PartitionInfo
    {
        public static Dictionary<ushort, PartitionDescriptor> data =
            new Dictionary<ushort, PartitionDescriptor>()
            {
                { 0, new PartitionDescriptor(40, 12, 4, 1) },
                { 1, new PartitionDescriptor(37, 11, 7, 2) },
                { 2, new PartitionDescriptor(34, 10, 10, 3) },
                { 3, new PartitionDescriptor(30, 9, 14, 4) },
                { 4, new PartitionDescriptor(27, 8, 17, 5) },
                { 5, new PartitionDescriptor(24, 7, 20, 6) },
                { 6, new PartitionDescriptor(20, 6, 24, 7) }
            };
    }
}