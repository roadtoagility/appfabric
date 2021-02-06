import { Component } from '@angular/core';

@Component({
  selector: 'ngx-footer',
  styleUrls: ['./footer.component.scss'],
  template: `
    <span class="created-by">
      Created with ♥ by <b><a href="https://roadtoagility.net/" target="_blank">RoadToAgility Team</a></b> and template created by <b><a href="https://akveo.page.link/8V2f" target="_blank">Akveo</a></b> 2019
    </span>
    <div class="socials">
      <a href="https://github.com/roadtoagility" target="_blank" class="ion ion-social-github"></a>
      <a href="https://www.instagram.com/roadtoagility/?hl=en" target="_blank" class="ion ion-social-instagram"></a>
      <a href="https://www.youtube.com/c/RoadtoAgility/featured" target="_blank" class="ion ion-social-youtube"></a>
      <a href="https://t.me/roadtoagility" target="_blank" class="ion ion-social-youtube"></a>
    </div>
  `,
})
export class FooterComponent {
}
