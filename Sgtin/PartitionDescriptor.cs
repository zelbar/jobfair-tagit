using System;
using System.Collections.Generic;

namespace JobFair.Tagit.Sgtin
{
    class PartitionDescriptor
    {
        public ushort CompanyPrefixBits { get; set; }
        public ushort CompanyPrefixDigits { get; set; }
        public ushort PadAndItemReferenceBits { get; set;}
        public ushort PadAndItemReferenceDigits { get; set; }

        public PartitionDescriptor(
            ushort companyPrefixBits,
            ushort companyPrefixDigits,
            ushort padAndItemReferenceBits,
            ushort padAndItemReferenceDigits)
            {
                CompanyPrefixBits = companyPrefixBits;
                CompanyPrefixDigits = companyPrefixDigits;
                PadAndItemReferenceBits = padAndItemReferenceBits;
                PadAndItemReferenceDigits = padAndItemReferenceDigits;
            }
    }
}