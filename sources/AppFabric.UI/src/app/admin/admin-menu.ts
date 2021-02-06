import { NbMenuItem } from '@nebular/theme';

export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: 'Dashboard',
    icon: 'home-outline',
    link: '/admin/dashboard',
    home: true,
  },
  {
    title: 'Clients',
    icon: 'people-outline',
    link: '/admin/clients',
    home: true,
  },
  {
    title: 'Projects',
    icon: 'clipboard-outline',
    link: '/admin/projects',
    home: true,
  },
  {
    title: 'Activities',
    icon: 'file-text-outline',
    link: '/admin/activities',
    home: true,
  },
  {
    title: 'Releases',
    icon: 'cube-outline',
    link: '/admin/releases',
    home: true,
  },
  {
    title: 'Billing',
    icon: 'trending-up-outline',
    link: '/admin/billings',
    home: true,
  }
];
