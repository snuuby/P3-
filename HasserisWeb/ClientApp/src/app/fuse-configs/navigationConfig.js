import {authRoles} from 'app/auth';

const navigationConfig = [
    {
        'id'      : 'applications',
        'title'   : 'Menu',
        'type'    : 'group',
        'icon'    : 'apps',
        'children': [
            {
                'id'   : 'dashboard',
                'title': 'Dashboard',
                'type' : 'item',
                'icon' : 'dashboard',
                'url'  : '/apps/calendar'
            },
            {
                'id': 'overview',
                'title': 'Overblik',
                'type': 'item',
                'icon': 'today',
                'url': ''
            },
            {
                auth: authRoles.admin,
                auth: authRoles.adminPlus,
                'id': 'createnew',
                'title': 'Opret ny',
                'type': 'collapse',
                'icon': 'dashboard',
                'children': [
                    {
                        'id': 'createinspectionreport',
                        'title': 'Besigtigelsesreport',
                        'type': 'item',
                        'url': ''
                    },
                    {
                        'id': 'createoffer',
                        'title': 'Tilbud',
                        'type': 'item',
                        'url': ''
                    },
                    {
                        'id': 'createtask',
                        'title': 'Opgave',
                        'type': 'item',
                        'url': ''
                    }
                ]
            },
            {
                'id': 'messages',
                'title': 'Beskeder',
                'type': 'item',
                'icon': 'message',
                'url': '/apps/mail/inbox'
            },
            {
                'id': 'notifications',
                'title': 'Notifikationer',
                'type': 'item',
                'icon': 'message',
                'url': ''
            },
            {
                auth: authRoles.adminPlus,
                'id': 'Economic',
                'title': 'Oekonomi',
                'type': 'item',
                'icon': 'money',
                'url': ''
            },
            {
                auth: authRoles.admin,
                auth: authRoles.adminPlus,
                'id': 'storage',
                'title': 'Lager',
                'type': 'item',
                'icon': 'storage',
                'url': ''
            },
            {
                auth: authRoles.admin,
                auth: authRoles.adminPlus,
                'id': 'customers',
                'title': 'Kundekartotek',
                'type': 'item',
                'icon': 'person',
                'url': '/customer/overview'
            },
            {
                auth: authRoles.adminPlus,
                'id': 'employees',
                'title': 'Ansatte',
                'type': 'item',
                'icon': 'person',
                'url': '/employee/overview'
            },
            {
                auth: authRoles.admin,
                auth: authRoles.adminPlus,
                'id': 'tool',
                'title': 'Udstyr',
                'type': 'item',
                'icon': 'list',
                'url': '/tool/overview'
            },
            {
                auth: authRoles.admin,
                auth: authRoles.adminPlus,
                'id': 'vehicles',
                'title': 'Koeretoejer',
                'type': 'item',
                'icon': 'list',
                'url': '/vehicle/overview'
            },
            ]}];
export default navigationConfig;
