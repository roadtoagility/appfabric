import { NbMenuItem } from '@nebular/theme';

export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: 'Dashboard',
    icon: 'home-outline',
    link: '/admin/dashboard',
    home: true,
  },
  {
    title: 'Clientes',
    icon: 'people-outline',
    link: '/admin/clientes',
    home: true,
  },
  {
    title: 'Projetos',
    icon: 'clipboard-outline',
    link: '/admin/projetos',
    home: true,
  },
  {
    title: 'Atividades',
    icon: 'file-text-outline',
    link: '/admin/atividades',
    home: true,
  },
  {
    title: 'Releases',
    icon: 'cube-outline',
    link: '/admin/releases',
    home: true,
  },
  {
    title: 'Financeiro',
    icon: 'trending-up-outline',
    link: '/admin/billings',
    home: true,
  }
];
