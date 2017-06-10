using System;
using System.Security.Cryptography;

namespace FileChecksum
{
	public abstract class CRC32 : HashAlgorithm
	{
		public uint CRC32Hash { get; protected set; }

		protected CRC32()
		{
			HashSizeValue = 32;
		}

		public new static CRC32 Create()
		{
			return new CRC32Managed();
		}

		public static CRC32 Create(uint polynomial)
		{
			return new CRC32Managed(polynomial);
		}

		public new static CRC32 Create(string hashName)
		{
			throw new NotImplementedException();
		}
	}

	public class CRC32Managed : CRC32
	{
		private readonly uint[] crc32Table = new uint[256];
		private uint crc32Result;

		public CRC32Managed()
			: this(0xEDB88320)
		{
		}

		public CRC32Managed(uint polynomial)
		{
			for (uint i = 0; i < 256; i++)
			{
				var crc32 = i;
				for (var j = 8; j > 0; j--)
				{
					if ((crc32 & 1) == 1)
					{
						crc32 = (crc32 >> 1) ^ polynomial;
					}
					else
					{
						crc32 >>= 1;
					}
				}
				crc32Table[i] = crc32;
			}

			Initialize();
		}

		public override bool CanReuseTransform => true;

		public override bool CanTransformMultipleBlocks => true;

		public override void Initialize()
		{
			crc32Result = 0xFFFFFFFF;
		}

		protected override void HashCore(byte[] array, int start, int size)
		{
			var end = start + size;
			for (var i = start; i < end; i++)
			{
				crc32Result = (crc32Result >> 8) ^ crc32Table[array[i] ^ (crc32Result & 0x000000FF)];
			}
		}

		protected override byte[] HashFinal()
		{
			crc32Result = ~crc32Result;

			CRC32Hash = crc32Result;

			HashValue = BitConverter.GetBytes(crc32Result);

			return HashValue;
		}
	}
}
