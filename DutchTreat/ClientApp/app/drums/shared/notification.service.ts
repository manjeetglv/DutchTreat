import { Injectable } from '@angular/core';
import {ToastrService} from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService) {}

  showSuccess(message, title){
    this.toastr.success(message, title, this.toasterOptions())
  }

  showError(message, title){
    this.toastr.error(message, title, this.toasterOptions())
  }

  showInfo(message, title){
    this.toastr.info(message, title, this.toasterOptions())
  }

  showWarning(message, title){
    this.toastr.warning(message, title, this.toasterOptions())
  }
  
  toasterOptions():{}{
    return {
      "closeButton": true,
      "debug": false,
      "newestOnTop": false,
      "progressBar": false,
      "positionClass": "toast-bottom-full-width",
      "preventDuplicates": false,
      "onclick": null,
      "showDuration": "300",
      "hideDuration": "1000",
      "timeOut": "5000",
      "extendedTimeOut": "1000",
      "showEasing": "swing",
      "hideEasing": "linear",
      "showMethod": "fadeIn",
      "hideMethod": "fadeOut"
    }
  }
}
