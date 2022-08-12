namespace DAL
{
    public class IDGenerator
    {
        private int _counter;
        private readonly Context _context;
        private readonly string _name;

        public IDGenerator(Context context, string name)
        {
            _context = context;
            _name = name;
            _counter = _context.ContextData.IDs[name];
        }

        public int GetNextID(bool forceSave = false)
        { var res = _counter;
            _counter++;
            _context.ContextData.IDs[_name] = _counter;
            
            if (forceSave) _context.JSON_Serialize();
            
            return res;
        }
    }
}