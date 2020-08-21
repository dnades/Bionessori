using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bionessori.Core.Extensions {
    public class Utf8StringWritterExtension : StringWriter {
        public override Encoding Encoding {
            get {
                return Encoding.UTF8;
            }
        }
    }
}