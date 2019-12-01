import {authRoles} from 'app/auth';

const navigationConfig = [
    {
        'id'      : 'applications',
        'title'   : 'Menu',
        'type'    : 'group',
        'icon'    : 'apps',
        'children': [
            {
                'id'   : 'calendar',
                'title': 'Kalender',
                'type' : 'item',
                'icon' : 'today',
                'url'  : '/apps/calendar'
            },
            {
                auth: authRoles.admin,
                auth: authRoles.adminPlus,
                'id': 'createnewtask',
                'title': 'Opret ny opgave',
                'type': 'collapse',
                'icon': 'dashboard',
                'children': [
                    {
                        'id': 'createinspectionreport',
                        'title': 'Besigtigelsesreport',
                        'type': 'item',
                        'url': '/InspectionReport/Make'
                    },
                    {
                        'id': 'createoffer',
                        'title': 'Tilbud',
                        'type': 'item',
                        'url': '/Offers/Create'
                    },
                    {
                        'id': 'createtask',
                        'title': 'Opgave',
                        'type': 'item',
                        'url': '/tasks/create'
                    }
                ]
            },
            {
                'id': 'taskoverview',
                'title': 'Opgave oversigt',
                'type': 'collapse',
                'icon': 'list',
                'children': [
                    {
                        'id': 'inspectionreportoverview',
                        'title': 'Besigtigelsesreporter',
                        'type': 'item',
                        'url': '/inspections/overview'
                    },
                    {
                        'id': 'offeroverview',
                        'title': 'Tilbud',
                        'type': 'item',
                        'url': '/offers/overview'
                    },
                    {
                        'id': 'taskoverview',
                        'title': 'Opgaver',
                        'type': 'item',
                        'url': '/tasks/overview'
                    }
                ]
            },
            {
                auth: authRoles.admin,
                auth: authRoles.adminPlus,
                'id': 'createnewressource',
                'title': 'Opret ny ressource',
                'type': 'collapse',
                'icon': 'dashboard',
                'children': [
                    {
                        'id': 'createcustomer',
                        'title': 'Opret kunde',
                        'type': 'item',
                        'url': '/customer/create'
                    },
                    {
                        'id': 'createemployee',
                        'title': 'Opret ansat',
                        'type': 'item',
                        'url': '/employee/create'
                    },
                    {
                        'id': 'createtool',
                        'title': 'Opret udstyr',
                        'type': 'item',
                        'url': '/tool/create'
                    },
                    {
                        'id': 'createvehicle',
                        'title': 'Opret køretøj',
                        'type': 'item',
                        'url': '/vehicle/create'
                    }
                ]
            },
           
            {
                auth: authRoles.admin,
                auth: authRoles.adminPlus,
                'id': 'resourceoverview',
                'title': 'Ressource oversigt',
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
            ]}];
export default navigationConfig;
