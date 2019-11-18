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
                'id': 'taskoverview',
                'title': 'Opgave oversigt',
                'type': 'collapse',
                'icon': 'today',
                'children': [
                    {
                        'id': 'inspectionreportoverview',
                        'title': 'Besigtigelsesreporter',
                        'type': 'item',
                        'url': ''
                    },
                    {
                        'id': 'offeroverview',
                        'title': 'Tilbud',
                        'type': 'item',
                        'url': ''
                    },
                    {
                        'id': 'taskoverview',
                        'title': 'Opgaver',
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
                'id': 'resourceoverview',
                'title': 'Oversigt',
                'type': 'collapse',
                'icon': 'list',
                'children': [
                    {
                        'id': 'customeroverview',
                        'title': 'Kundekartotek',
                        'type': 'item',
                        'url': '/customer/overview'
                    },
                    {
                        auth: authRoles.adminPlus,
                        'id': 'employees',
                        'title': 'Ansatte',
                        'type': 'item',
                        'url': '/employee/overview'
                    },
                    {
                        'id': 'tool',
                        'title': 'Udstyr',
                        'type': 'item',
                        'url': '/tool/overview'
                    },
                    {
                        'id': 'vehicles',
                        'title': 'Koeretoejer',
                        'type': 'item',
                        'url': '/vehicle/overview'
                    }
                ]
            },
            ]}];
export default navigationConfig;
