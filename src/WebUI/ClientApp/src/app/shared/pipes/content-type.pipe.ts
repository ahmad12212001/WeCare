import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'contenttype'
})
export class ContentTypePipe implements PipeTransform {

  transform(value: string, path: string): string {
    switch (value) {
      case 'image/png':
      case 'image/jpeg':
      case 'image/bmp':
      case 'image/gif':
      case 'image/svg+xml':
        return path;
      case 'application/pdf':
        return "./assets/images/media/files/pdf.png";
      case 'application/vnd.ms-excel':
        return './assets/images/media/files/excel.png';
      case 'application/msword':
      case 'application/vnd.ms-works':
      case 'application/x-cdf':
      case 'application/x-latex':
      case 'application/x-netcdf':
      case 'application/x-tex':
      case 'application/x-texinfo':
      case 'application/x-texinfo':
      case 'application/text':
        return './assets/images/media/files/word.png';
      case 'application/vnd.ms-powerpoint':
        return "./assets/images/media/files/ppt1.png";
      case 'video/mpeg':
      case 'video/mpeg':
      case 'video/mpeg':
      case 'video/mpeg':
      case 'video/mpeg':
      case 'video/mpeg':
      case 'video/mp4':
      case 'video/quicktime':
      case 'video/quicktime':
      case 'video/x-la-asf':
      case 'video/x-la-asf':
      case 'video/x-ms-asf':
      case 'video/x-ms-asf':
      case 'video/x-ms-asf':
      case 'video/x-msvideo':
      case 'video/x-sgi-movie':
      case 'audio/basic':
      case 'audio/mid':
      case 'audio/mid':
      case 'audio/mpeg':
      case 'audio/x-aiff':
      case 'audio/x-aiff':
      case 'audio/x-aiff':
      case 'audio/x-mpegurl':
      case 'audio/x-pn-realaudio':
      case 'audio/x-pn-realaudio':
      case 'audio/x-wav':
        return './assets/images/media/files/video.png';
    }
    return './assets/images/media/files/default.png';
  }

}
