﻿using NHibernate;
using System;

namespace Infrastructure.NHibernate
{
    public class NhUnitOfWork : IUnitOfWork
    {
        public static NhUnitOfWork Current
        {
            get { return _current; }
            set { _current = value; }
        }

        [ThreadStatic]
        private static NhUnitOfWork _current;

        public ISession Session { get; private set; }

        private readonly ISessionFactory _sessionFactory;

        private ITransaction _transaction;

        public NhUnitOfWork(ISessionFactory sessionFactory)
        {
            if (sessionFactory == null) throw new ArgumentNullException("sessionFactory");
            _sessionFactory = sessionFactory;
        }

        public void BeginTransaction()
        {
            Session = _sessionFactory.OpenSession();
            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            finally
            {
                Session.Close();                
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            finally
            {
                Session.Close();                
            }
        }
    }
}