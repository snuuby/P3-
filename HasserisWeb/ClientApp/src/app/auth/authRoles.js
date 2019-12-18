/**
 * Authorization Roles
 */
const authRoles = {
    adminPlus    : ['adminplus'],
    admin        : ['admin', 'adminplus'],
    employee     : ['admin', 'adminplus', 'employee'],
    onlyGuest: []
};

export default authRoles;
