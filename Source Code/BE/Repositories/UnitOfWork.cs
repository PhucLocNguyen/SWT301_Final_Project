using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Entity;

namespace Repositories

{
    public class UnitOfWork
    {
        private MyDbContext _context;
        private GenericRepository<Blog> _blog;
        private GenericRepository<Design> _design;
        private GenericRepository<DesignRule> _designRule;
        private GenericRepository<Have> _have;
        private GenericRepository<MasterGemstone> _masterGemstone;
        private GenericRepository<Material> _material;
        private GenericRepository<Payment> _payment;
        private GenericRepository<Requirement> _requirement;
        private GenericRepository<Stones> _stone;
        private GenericRepository<TypeOfJewellery> _typeOfJewellry;
        private GenericRepository<Users> _user;
        private GenericRepository<Role> _role;
        private GenericRepository<WarrantyCard> _warrantyCard;


        public UnitOfWork(MyDbContext context)
        {
            _context = context;
        }

        public GenericRepository<Blog> BlogRepository
        {
            get
            {
                if (_blog == null)
                {
                    this._blog = new GenericRepository<Blog>(_context);
                }
                return _blog;
            }

        }
        public GenericRepository<Design> DesignRepository
        {
            get
            {
                if (_design == null)
                {
                    this._design = new GenericRepository<Design>(_context);
                }
                return _design;
            }

        }
        public GenericRepository<DesignRule> DesignRuleRepository
        {
            get
            {
                if (_designRule == null)
                {
                    this._designRule = new GenericRepository<DesignRule>(_context);
                }
                return _designRule;
            }

        }
        public GenericRepository<Have> HaveRepository
        {
            get
            {
                if (_have == null)
                {
                    this._have = new GenericRepository<Have>(_context);
                }
                return _have;
            }

        }
        public GenericRepository<MasterGemstone> MasterGemstoneRepository
        {
            get
            {
                if (_masterGemstone == null)
                {
                    this._masterGemstone = new GenericRepository<MasterGemstone>(_context);
                }
                return _masterGemstone;
            }

        }
        public GenericRepository<Material> MaterialRepository
        {
            get
            {
                if (_material == null)
                {
                    this._material = new GenericRepository<Material>(_context);
                }
                return _material;
            }

        }
        public GenericRepository<Payment> PaymentRepository
        {
            get
            {
                if (_payment == null)
                {
                    this._payment = new GenericRepository<Payment>(_context);
                }
                return _payment;
            }

        }
        public GenericRepository<Requirement> RequirementRepository
        {
            get
            {
                if (_requirement == null)
                {
                    this._requirement = new GenericRepository<Requirement>(_context);
                }
                return _requirement;
            }

        }
        public GenericRepository<Stones> StoneRepository
        {
            get
            {
                if (_stone == null)
                {
                    this._stone = new GenericRepository<Stones>(_context);
                }
                return _stone;
            }

        }
        public GenericRepository<TypeOfJewellery> TypeOfJewellryRepository
        {
            get
            {
                if (_typeOfJewellry == null)
                {
                    this._typeOfJewellry = new GenericRepository<TypeOfJewellery>(_context);
                }
                return _typeOfJewellry;
            }

        }
        public GenericRepository<Users> UserRepository
        {
            get
            {
                if (_user == null)
                {
                    this._user = new GenericRepository<Users>(_context);
                }
                return _user;
            }

        }
        public GenericRepository<Role> RoleRepository
        {
            get
            {
                if (_role == null)
                {
                    this._role = new GenericRepository<Role>(_context);
                }
                return _role;
            }

        }
        public GenericRepository<WarrantyCard> WarrantyCardRepository
        {
            get
            {
                if (_warrantyCard == null)
                {
                    this._warrantyCard = new GenericRepository<WarrantyCard>(_context);
                }
                return _warrantyCard;
            }

        }


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public bool IsForeignKeyConstraintViolation(DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlException)
            {
                // Error number 547 corresponds to a foreign key constraint violation in SQL Server
                return sqlException.Number == 547;
            }

            return false;
        }
    }
}
