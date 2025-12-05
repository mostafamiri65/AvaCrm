export enum UnitCategory {
  General = 0,   // واحدهای عمومی
  Count = 1,     // تعداد (عدد، جین، بسته)
  Weight = 2,    // وزن (کیلوگرم، گرم، تن)
  Length = 3,    // طول (متر، سانتیمتر، کیلومتر)
  Area = 4,      // مساحت (متر مربع)
  Volume = 5,    // حجم (لیتر، میلی‌لیتر)
  Time = 6       // زمان (ساعت، روز - در صورت نیاز)
}

export interface UnitDto {
  id: number;
  name: string;
  code: string;
  category: UnitCategory;
}

export interface CreateUnitDto {
  name: string;
  code: string;
  category: UnitCategory;
}

export interface UpdateUnitDto {
  id: number;
  name: string;
  code: string;
  category: UnitCategory;
}
