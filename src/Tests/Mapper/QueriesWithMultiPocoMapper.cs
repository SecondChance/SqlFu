﻿using CavemanTools.Model;
using SqlFu;
using Xunit;
using System;
using System.Diagnostics;
using System.Linq;

namespace Tests.Mapper
{
    public class QueriesWithMultiPocoMapper
    {
        private Stopwatch _t = new Stopwatch();
        private SqlFuConnection _db;

        public QueriesWithMultiPocoMapper()
        {
            _db = Config.GetDb();
        }

        [Fact]
        public void default_mapper()
        {
            var pv =
                _db.Query<PostViewModel>("select 1 as Id, 2 as Author_Id, 'test' as Author_Name,null as Topic").First();
            Assert.Equal(1,pv.Id);
            Assert.Equal(2,pv.Author.Id);
            Assert.Equal("test",pv.Author.Name);
            Assert.Null(pv.Topic);
        }

        [Fact]
        public void custom_instnatiator()
        {
            DefaultComplexTypeMapper.ToCreate<IdName>(p=>
                                                         {
                                                             return new IdName(){Name=p.Title};
                                                         });
            var pv=_db.Query<PostViewModel>("select 1 as Id, 'test' as Title, 3 as Author_Id").First();
            Assert.Equal(1,pv.Id);
            Assert.Equal("test",pv.Author.Name);
            
        }

        private void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}