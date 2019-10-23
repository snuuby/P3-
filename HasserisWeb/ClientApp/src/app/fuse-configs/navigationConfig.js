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
                'url': ''
            },
            {
                'id': 'notifications',
                'title': 'Notifikationer',
                'type': 'item',
                'icon': 'message',
                'url': ''
            },
            {
                'id': 'Economic',
                'title': 'Oekonomi',
                'type': 'item',
                'icon': 'money',
                'url': ''
            },
            {
                'id': 'storage',
                'title': 'Lager',
                'type': 'item',
                'icon': 'storage',
                'url': ''
            },
            {
                'id': 'customers',
                'title': 'Kundekartotek',
                'type': 'item',
                'icon': 'person',
                'url': ''
            },
            {
                'id': 'employees',
                'title': 'Ansatte',
                'type': 'item',
                'icon': 'person',
                'url': '/employee/overview'
            },
            {
                'id': 'equipment',
                'title': 'Udstyr',
                'type': 'item',
                'icon': 'list',
                'url': ''
            },
            {
                'id': 'vehicles',
                'title': 'K�ret�jer',
                'type': 'item',
                'icon': 'list',
                'url': ''
            },
            ]}];
export default navigationConfig;
