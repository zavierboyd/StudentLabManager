function createschema(classlist) {
    const schema = {
        "title": "Test Schedule",
        "type": "object",
        "properties": {
            "exam": {
                "required": true,
                "title": "Exam",
                "pattern": ".{1,}",
                "type": "string"
            },
            "group": {
                "required": true,
                "title": "Class",
                "type": "string",
                "enum": ["Default1", "Default2", "ShouldNotBeHere"]
            },
            "duration": {
                "required": true,
                "title": "Duration in minutes",
                "minimum": 1,
                "type": "integer",
                "format": "number"
            },
            "schedule": {
                "required": true,
                "title": "Schedule",
                "type": "array",
                "format": "tabs",
                "minItems": 1,
                "items": {
                    "required": true,
                    "title": "Day",
                    "headerTemplate": "Day - {{i1}} | {{self.date}}",
                    "type": "object",
                    "properties": {
                        "date": {
                            "required": true,
                            "title": "Date",
                            "type": "string",
                            "format": "date"
                        },

                        "timeSlots": {
                            "required": true,
                            "type": "array",
                            "title": "Time Slots",
                            "format": "tabs-top",
                            "minItems": 1,
                            "items": {
                                "required": true,
                                "title": "Time Slot",
                                "headerTemplate": "Slot - {{self.startTime}}",
                                "type": "object",
                                "properties": {
                                    "startTime": {
                                        "required": true,
                                        "title": "Start Time",
                                        "type": "string",
                                        "pattern": ".{1,}",
                                        "format": "time"
                                    },
                                    "studentNo": {
                                        "required": true,
                                        "type": "integer",
                                        "minimum": 1,
                                        "title": "Number of Students"
                                    },
                                    "place": {
                                        "type": "string",
                                        "required": true,
                                        "pattern": ".{1,}",
                                        "title": "Where is the exam?"

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    schema.properties.group.enum = classlist;
    return schema;
}