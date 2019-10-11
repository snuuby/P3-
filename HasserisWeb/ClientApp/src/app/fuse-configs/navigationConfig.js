const navigationConfig = [
    {
        'id'      : 'applications',
        'title'   : 'Applications',
        'type'    : 'group',
        'icon'    : 'apps',
        'children': [
            {
                'id'   : 'example-component',
                'title': 'Example',
                'type' : 'item',
                'icon' : 'whatshot',
                'url'  : '/example'
            },
            {
                'id': 'calendar-component',
                'title': 'Calendar',
                'type': 'item',
                'icon': 'whatshot',
                'url': '/apps/calendar'
            }
        ]
    }
];

export default navigationConfig;
