import { HttpInterceptorFn } from '@angular/common/http';
import {ApiAddressUtility} from '../utilities/api-address.utility';

export const baseUrlInterceptor: HttpInterceptorFn = (req, next) => {
  if (!req.url.startsWith("http")) {
    let modifiedUrl = req.clone({
      url: `${ApiAddressUtility.BaseAddress}${req.url}`
    })

    return next(modifiedUrl)
  } else {
    return next(req);
  }
};
