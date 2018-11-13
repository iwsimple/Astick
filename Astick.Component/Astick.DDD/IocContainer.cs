using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astick.DDD
{
    //静态类不可以被实例化，但是静态变量可以被赋值
    public class IocContainer
    {
        private IWindsorContainer _windsor;
        //IocContainer类型的静态对象_container可以被赋值
        private static IocContainer _container = null;
        private static readonly object _lock=new object();

        public IocContainer()
        {
            //注册
            _windsor = new WindsorContainer().Install(
                Configuration.FromAppConfig(),
                FromAssembly.This()
                );
        }

        //用作初始化Ioc对象
        //不明白为什么用静态变量而不用方法
        public static IocContainer Instance
        {
            get
            {
                //系统运行时，_container只会存在一个
                lock (_lock)
                {
                    //调用构造函数注册
                    if (_container == null)
                        _container = new IocContainer();
                    return _container;
                }
            }
        }

        public static IWindsorContainer GetContainer()
        {
            //需实例化IocContainer类，调用成员变量_windsorContainer;
            //如果_windsor为静态变量，Instance返回的Ioc类型变量,是不可以通过实例访问类内的静态变量的，
            //因此只能在ioc类内部直接调用_windsor变量。而这样就会调用方法就注册windsor，与设计违背。
            //在应用程序执行期间，只注册一次_windsor,将配置加载。需要用静态属性Instance来控制Ioc类的初始化，赋值给静态对象_container
            //用IocContainer对象_contianer来调用IocContainer内部对象_windsor,来构造派生类。
            return Instance._windsor;
        }

        //静态方法，直接.加方法名调用
        //*静态方法只能调用静态方法*
        //Get方法的作用获取IwindsorContainer对象注册T的实现类
        public static T Get<T>()
        {
            //获取IwindsorContainer对象
            return GetContainer().Resolve<T>();
        }

    }
}
