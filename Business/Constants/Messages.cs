using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants//Contants - Sabitler demek. Enum gibi
{//Temel mesajlarımızı bunun içine yazacağız.
    public static class Messages //New'lememek için static verdik.Basit bir mesaj old. için ve sabit old. için newlemek istemiyoruz ve static yapıyoruz.
    {
        public static string ProductAdded = "Ürün eklendi";//bir değişken olmasına rağmen büyük harfle yazdık. Nedeni Public olması publicler pascalcase ile yazılır.
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir.";
        public static string ProductNameAlreadyExists = "Ürün ismi zaten mevcut";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
    }
}
