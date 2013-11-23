﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alicargo.Core.Resources {
	/// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Mail {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Mail() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Alicargo.Core.Resources.Mail", typeof(Mail).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Создана новая заявка.
        ///Номер заявки: {0}
        ///Клиент: {1}
        ///Производитель: {2}
        ///Марка: {3}
        ///Дата создания:{4}
        ///
        ///
        ///Created new order.
        ///Number order: {0}
        ///Client: {1}
        ///Factory: {2}
        ///Brands: {3}
        ///Created:{4}.
        /// </summary>
        public static string Application_Add {
            get {
                return ResourceManager.GetString("Application_Add", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Добрый день, {0}
        ///Изменение статуса заявки {1} - прикреплен документ Счет-фактура.
        ///Просьба подписать и выслать почтой по адресу: п/о Домодедово-4 а/я 649 (142004, Московская область, Домодедовский район, г. Домодедово, ул. Корнеева 36.
        ///
        ///С уважением,
        ///Alicargo srl
        ///
        ///
        ///Hello, {0}
        ///Changing the status of the application {1}. Commercial invoice is attached.
        ///Please sign and send by mail to: Post Office Domodedovo-4 PO Box 649 (142004, Moscow region, Domodedovo district, Domodedovo, st. Korneev 36.
        ///
        ///Sincerely,
        ///Alicarg [rest of string was truncated]&quot;;.
        /// </summary>
        public static string Application_CPFileAdded {
            get {
                return ResourceManager.GetString("Application_CPFileAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявка удалена..
        /// </summary>
        public static string Application_Delete {
            get {
                return ResourceManager.GetString("Application_Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса заявки: {0}, {1}, {2}, {3}, добавлен счет за доставку
        ///
        ///Changing the status of the application: {0}, {1}, {2}, {3}, added due for delivery.
        /// </summary>
        public static string Application_DeliveryBillFileAdded {
            get {
                return ResourceManager.GetString("Application_DeliveryBillFileAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса заявки: {0}, {1}, {2}, {3}, добавлен инвойс {4}
        ///
        ///Changing the status of the application: {0}, {1}, {2}, {3}, added invoice {4}.
        /// </summary>
        public static string Application_InvoiceFileAdded {
            get {
                return ResourceManager.GetString("Application_InvoiceFileAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса заявки: {0}, {1}, {2}, {3}, добавлен пакинг
        ///
        ///Changing the status of the application: {0}, {1}, {2}, {3}, added paking.
        /// </summary>
        public static string Application_PackingFileAdded {
            get {
                return ResourceManager.GetString("Application_PackingFileAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Предположительная дата получения — {0}.
        /// </summary>
        public static string Application_SetDateOfCargoReceipt {
            get {
                return ResourceManager.GetString("Application_SetDateOfCargoReceipt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса заявки: {34}
        ///Номер заказа: {0}
        ///Предположительная дата получения: {1}
        ///Перевозчик: {2}
        ///Производитель: {3}
        ///Email фабрики: {4}
        ///Телефон фабрики: {5}
        ///Контактное лицо фабрики: {6}
        ///Дней в работе: {7}
        ///Инвойс: {8}
        ///Дата создания: {9}
        ///Дата сметы статуса: {10}
        ///Марка: {11}
        ///Количество мест (коробки): {12}
        ///Объем (m3 ): {13}
        ///Вес (кг): {14}
        ///Характеристика товара: {15}
        ///Стоимость: {16}
        ///Адрес забора груза: {17}
        ///Страна: {18}
        ///Время работы склада: {19}
        ///Условия поставки: {20}
        ///Способ доставки: {21}
        ///Транзитный рефер [rest of string was truncated]&quot;;.
        /// </summary>
        public static string Application_SetState {
            get {
                return ResourceManager.GetString("Application_SetState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса заявки: {0}, {1}, {2}, {3}. Задан транзитный референс {4}
        ///
        ///Changing the status of the application: {0}, {1}, {2}, {3}. Specified transit reference {4}.
        /// </summary>
        public static string Application_SetTransitReference {
            get {
                return ResourceManager.GetString("Application_SetTransitReference", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Alicargo: Заявка/Order #{0}.
        /// </summary>
        public static string Application_Subject {
            get {
                return ResourceManager.GetString("Application_Subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса заявки: {0}, {1}, {2}, {3}, добавлен swift
        ///
        ///Changing the status of the application: {0}, {1}, {2}, {3}, added a swift.
        /// </summary>
        public static string Application_SwiftFileAdded {
            get {
                return ResourceManager.GetString("Application_SwiftFileAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Добрый день, {0}
        ///Изменение статуса заявки {1} - прикреплен документ ТОРГ-12.
        ///Просьба подписать и выслать почтой по адресу: п/о Домодедово-4 а/я 649 (142004, Московская область, Домодедовский район, г. Домодедово, ул. Корнеева 36.
        ///
        ///С уважением,
        ///Alicargo srl
        ///
        ///
        ///Hello, {0}
        ///Changing the status of the application {1}. TORG-12 is attached.
        ///Please sign and send by mail to: Post Office Domodedovo-4 PO Box 649 (142004, Moscow region, Domodedovo district, Domodedovo, st. Korneev 36.
        ///
        ///Sincerely,
        ///Alicargo srl.
        /// </summary>
        public static string Application_Torg12FileAdded {
            get {
                return ResourceManager.GetString("Application_Torg12FileAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявка обновлена..
        /// </summary>
        public static string Application_Update {
            get {
                return ResourceManager.GetString("Application_Update", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса  AWB: Номер авиа-накладной {0}, добавлена авиа-накладная
        ///
        ///Changing the status of AWB: air waybill number {0}, added air waybill.
        /// </summary>
        public static string Awb_AWBFileAdd {
            get {
                return ResourceManager.GetString("Awb_AWBFileAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Создана авианакладная, Аэропорт вылета: {0}/{1}, Аэропорт прилета: {2}/{3}, Вес: {4}, Количество мест: {5}. Номер накладной: {6}.
        /// </summary>
        public static string Awb_Create {
            get {
                return ResourceManager.GetString("Awb_Create", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса  AWB: Номер авиа-накладной {0}, «добавлен ГТД-дополнение»
        ///
        ///Changing the status of AWB: air waybill number {0}, &quot;added the GTD-complement&quot;.
        /// </summary>
        public static string Awb_GTDAdditionalFileAdd {
            get {
                return ResourceManager.GetString("Awb_GTDAdditionalFileAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса  AWB: Номер авиа-накладной {0}, «добавлен ГТД»
        ///
        ///Changing the status of AWB: air waybill number {0}, &quot;added GTD&quot;.
        /// </summary>
        public static string Awb_GTDFileAdd {
            get {
                return ResourceManager.GetString("Awb_GTDFileAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса  AWB: Номер авиа-накладной {0}, «добавлен инвойс»
        ///
        ///Changing the status of AWB: air waybill number {0}, &quot;added invoice&quot;.
        /// </summary>
        public static string Awb_InvoiceFileAdd {
            get {
                return ResourceManager.GetString("Awb_InvoiceFileAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изменение статуса  AWB: Номер авиа-накладной {0}, «добавлен пакинг-лист»
        ///
        ///Changing the status of AWB: air waybill number {0}, &quot;added paking-list&quot;.
        /// </summary>
        public static string Awb_PackingFileAdd {
            get {
                return ResourceManager.GetString("Awb_PackingFileAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявке {7} задана авианакладная, Аэропорт вылета: {0}/{1}, Аэропорт прилета: {2}/{3}, Вес: {4}, Количество мест: {5}. Номер накладной: {6}.
        /// </summary>
        public static string Awb_Set {
            get {
                return ResourceManager.GetString("Awb_Set", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Добрый день, {0}! Вы зарегистрированны в системе отслеживания грузов компании  Alicargo. Имя доступа: {1}, Пароль: {2}..
        /// </summary>
        public static string Client_Add {
            get {
                return ResourceManager.GetString("Client_Add", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Alicargo.
        /// </summary>
        public static string Default_Subject {
            get {
                return ResourceManager.GetString("Default_Subject", resourceCulture);
            }
        }
    }
}
