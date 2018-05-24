using System;
using JobFair.Tagit.Sgtin.Enums;
using JobFair.Tagit.Sgtin.Exceptions;

namespace JobFair.Tagit.Sgtin
{
    /// <summary>
    /// Stores all data about an item that are defined by its SGTIN
    /// </summary>
    public class Item
    {
        /// <summary>
        /// The SGTIN number string representation supplied in the constructor
        /// </summary>
        /// <returns>HEX code in string format</returns>
        public string SgtinNumber { get => _numberString; }

        /// <summary>
        /// SGTIN Header Value
        /// One byte that defines the used SGTIN coding scheme
        /// </summary>
        /// <returns>Byte value</returns>
        public byte Header { get => _numberBytes[0]; }

        /// <summary>
        /// SGTIN Coding Scheme
        /// Defines how long the SGTIN number is, that is, how many bytes it needs for storage
        /// </summary>
        /// <returns>Enumarable item from supported values</returns>
        public SgtinCodingScheme CodingScheme { get => (SgtinCodingScheme) _numberBytes[0]; }

        /// <summary>
        /// SGTIN Filter value (one byte), numeric, from 0 to 7 (decimal)
        /// </summary>
        /// <returns>Enumerable filter item</returns>
        public Filter Filter
        {
            get
            {
                return (Filter) (_numberBytes[1] >> 1);
            }
        }

        /// <summary>
        /// SGTIN Partition is a 3-bit value that determines how many out of 44 bits
        /// are spent on the company prefix and how many on the item reference
        /// </summary>
        /// <returns>Byte value with only lower 3 bits used</returns>
        public Byte Partition
        {
            get {
                return (Byte) ((_numberBytes[1] >> 2) & 0b111);
            }
        }

        /// <summary>
        /// Item GS1 Company Prefix
        /// Identifies a company with their officially registered GS1 company prefix
        /// </summary>
        /// <returns></returns>
        public string CompanyPrefix 
        {
            get
            {
                ushort bitsCount = PartitionInfo.data[Partition].CompanyPrefixBits,
                    digitsCount = PartitionInfo.data[Partition].CompanyPrefixDigits;

                // get bits 0-1 and make place for bits 2-9
                long data = (_numberBytes[1] & 0b11) << 8;
                // add bits 2-9
                data += _numberBytes[2];
                // make place for bits 10-17
                data <<= 8;
                // add bits 10-17
                data += _numberBytes[3];
                // make place for bits 18-25
                data <<= 8;
                // add bits 18-25
                data += _numberBytes[4];
                // make place for bits 26-33
                data <<= 8;
                // add bits 26-33
                data += _numberBytes[5];

                if (Partition == 3) 
                {
                    // arrange data to left
                    data >>= 4;
                }
                else if (Partition == 0) 
                {
                    // make place for bits 34-41
                    data <<= 8;
                    // add bits 34-41
                    data += _numberBytes[6];
                    // arrange data to left
                    data >>= 2;
                }
                
                // pad item with zeros to required number of digits
                return data.ToString("D" + digitsCount);
            }
        }

        /// <summary>
        /// Item Reference (Item Type)
        /// Identifies a product or product type
        /// </summary>
        /// <returns></returns>
        public string ItemReference 
        {
            get
            {
                // get bits 0-3 and make place for bits 4-11
                uint data = (uint) (_numberBytes[5] & 0b1111) << 8;
                // add bits 4-11
                data += _numberBytes[6];
                // make place for bits 12-13
                data <<= 2;
                // add bits 12-13
                data += (uint) (_numberBytes[7] >> 6);

                return data.ToString("D" + 
                    PartitionInfo.data[Partition].PadAndItemReferenceDigits);
            }
        }

        /// <summary>
        /// Item Serial Number
        /// Identifies an unique product instance
        /// </summary>
        /// <returns></returns>
        public string SerialNumber 
        {
            get
            {
                // get bits 0-5 and make space for bits 6-13
                long data = (_numberBytes[7] & 0b111111) << 8;
                // add bits 6-13
                data += _numberBytes[8];
                // make space for bits 14-21
                data <<= 8;
                // add bits 14-21
                data += _numberBytes[9];
                // make space for bits 22-29
                data <<= 8;
                // add bits 22-29
                data += _numberBytes[10];
                // make space for bits 30-37
                data <<= 8;
                // add bits 30-37
                data += _numberBytes[11];

                return data.ToString();
            }
        }

        //protected readonly SgtinCodingScheme _codingScheme;
        protected readonly string _numberString;

        protected Byte[] _numberBytes;

        /// <summary>
        /// Creates a new Item defined by a Serialized Global Trade Item Number (SGTIN)
        /// </summary>
        /// <param name="sgtinNumber">The SGTIN number string representation</param>
        public Item(string sgtinNumber)
        {
            _numberString = sgtinNumber;

            // Check if the header is in one of the supported SGTIN schemes
            try
            {
                //_codingScheme = (SgtinCodingScheme) 
                Convert.ToByte(_numberString[0].ToString(), 16);
            }
            catch (InvalidCastException)
            {
                throw new UnsupportedSgtinCodingSchemeException(
                    "The supported SGTIN coding schemes are SGTIN-64, SGTIN-96 and SGTIN-198"
                );
            }

            // Convert string code to bytes array

            int numOfBytes = _numberString.Length / 2,
                byteCounter = 0;
            _numberBytes = new byte[numOfBytes];

            for (int hexCharCounter = 0; byteCounter < numOfBytes; ++byteCounter)
            {
                byte firstHalfOfByte = Convert.ToByte(_numberString[hexCharCounter++].ToString(), 16),
                    secondHalfOfByte = Convert.ToByte(_numberString[hexCharCounter++].ToString(), 16);

                _numberBytes[byteCounter] = 
                    Convert.ToByte((firstHalfOfByte << 4) + secondHalfOfByte);
            }

            if (Partition != 3) {
                throw new NotImplementedException(
                    "Only SGTIN with partition value 3 is currently implemented"
                );
            }
        }
    }
}