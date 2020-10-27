using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UONegotiator.UOPacket;

namespace UONegotiator
{
    public enum PacketAction
    {
        DROP,
        FORWARD
    }

    public enum Source
    {
        CLIENT,
        SERVER
    }

    public class CMD
    {
        public const byte CREATE_CHARACTER                  = 0x00;
        public const byte DISCONNECT_NOTIFICATION           = 0x01;
        public const byte MOVE_REQUEST                      = 0x02;
        public const byte TALK_REQUEST                      = 0x03;
        public const byte REQUEST_GOD_MODE                  = 0x04;
        public const byte REQUEST_ATTACK                    = 0x05;
        public const byte DOUBLE_CLICK                      = 0x06;
        public const byte PICK_UP_ITEM                      = 0x07;
        public const byte DROP_ITEM                         = 0x08;
        public const byte SINGLE_CLICK                      = 0x09;
        public const byte EDIT                              = 0x0A;
        public const byte DAMAGE                            = 0x0B;
        public const byte EDIT_TILE_DATA                    = 0x0C;
        public const byte STATUS_BAR_INFO                   = 0x11;
        public const byte REQUEST_SKILL_ETC_USE             = 0x12;
        public const byte DROP_OR_WEAR_ITEM                 = 0x13;
        public const byte SEND_ELEVATION                    = 0x14;
        public const byte FOLLOW                            = 0x15;
        public const byte NEW_HEALTH_BAR_STATUS_UPDATE      = 0x16;
        public const byte HEALTH_BAR_STATUS_UPDATE          = 0x17;
        public const byte OBJECT_INFO                       = 0x1A;
        public const byte CHAR_LOCALE_AND_BODY              = 0x1B;
        public const byte SEND_SPEECH                       = 0x1C;
        public const byte DELETE_OBJECT                     = 0x1D;
        public const byte CONTROL_ANIMATION                 = 0x1E;
        public const byte EXPLOSION                         = 0x1F;
        public const byte DRAW_GAME_PLAYER                  = 0x20;
        public const byte CHAR_MOVE_REJECTION               = 0x21;
        public const byte CHARACTER_MOVE_ACK_RESYNC_REQUEST = 0x22;
        public const byte DRAGGING_OF_ITEM                  = 0x23;
        public const byte DRAW_CONTAINER                    = 0x24;
        public const byte ADD_ITEM_TO_CONTAINER             = 0x25;
        public const byte KICK_PLAYER                       = 0x26;
        public const byte REJECT_MOVE_ITEM_REQUEST          = 0x27;
        public const byte DROP_ITEM_FAILEDCLEAR_SQUARE      = 0x28;
        public const byte DROP_ITEM_APPROVED                = 0x29;
        public const byte BLOOD                             = 0x2A;
        public const byte GOD_MODE                          = 0x2B;
        public const byte RESURRECTION_MENU                 = 0x2C;
        public const byte MOB_ATTRIBUTES                    = 0x2D;
        public const byte WORN_ITEM                         = 0x2E;
        public const byte FIGHT_OCCURING                    = 0x2F;
        public const byte ATTACK_OK                         = 0x30;
        public const byte ATTACK_ENDED                      = 0x31;
        public const byte UNKNOWNX32                        = 0x32;
        public const byte PAUSE_CLIENT                      = 0x33;
        public const byte GET_PLAYER_STATUS                 = 0x34;
        public const byte ADD_RESOURCE                      = 0x35;
        public const byte RESOURCE_TILE_DATA                = 0x36;
        public const byte MOVE_ITEM                         = 0x37;
        public const byte PATHFINDING_IN_CLIENT             = 0x38;
        public const byte REMOVE                            = 0x39;
        public const byte SEND_SKILLS                       = 0x3A;
        public const byte BUY_ITEM                          = 0x3B;
        public const byte ADD_MULTIPLE_ITEMS_IN_CONTAINER   = 0x3C;
        public const byte VERSIONS                          = 0x3E;
        public const byte UPDATE_STATICS                    = 0x3F;
        public const byte VERSION_OK                        = 0x45;
        public const byte NEW_ARTWORK                       = 0x46;
        public const byte NEW_TERRAIN                       = 0x47;
        public const byte NEW_ANIMATION                     = 0x48;
        public const byte NEW_HUES                          = 0x49;
        public const byte DELETE_ART                        = 0x4A;
        public const byte CHECK_CLIENT_VERSION              = 0x4B;
        public const byte SCRIPT_NAMES                      = 0x4C;
        public const byte EDIT_SCRIPT_FILE                  = 0x4D;
        public const byte PERSONAL_LIGHT_LEVEL              = 0x4E;
        public const byte OVERALL_LIGHT_LEVEL               = 0x4F;
        public const byte BOARD_HEADER                      = 0x50;
        public const byte BOARD_MESSAGE                     = 0x51;
        public const byte BOARD_POST_MESSAGE                = 0x52;
        public const byte REJECT_CHARACTER_LOGON            = 0x53;
        public const byte PLAY_SOUND_EFFECT                 = 0x54;
        public const byte LOGIN_COMPLETE                    = 0x55;
        public const byte MAP_PACKET                        = 0x56;
        public const byte UPDATE_REGIONS                    = 0x57;
        public const byte ADD_REGION                        = 0x58;
        public const byte NEW_CONTEXT_FX                    = 0x59;
        public const byte UPDATE_CONTEXT_FX                 = 0x5A;
        public const byte TIME                              = 0x5B;
        public const byte RESTART_VERSION                   = 0x5C;
        public const byte LOGIN_CHARACTER                   = 0x5D;
        public const byte SERVER_LISTING                    = 0x5E;
        public const byte SERVER_LIST_ADD_ENTRY             = 0x5F;
        public const byte SERVER_LIST_REMOVE_ENTRY          = 0x60;
        public const byte REMOVE_STATIC_OBJECT              = 0x61;
        public const byte MOVE_STATIC_OBJECT                = 0x62;
        public const byte LOAD_AREA                         = 0x63;
        public const byte LOAD_AREA_REQUEST                 = 0x64;
        public const byte SET_WEATHER                       = 0x65;
        public const byte BOOKS                             = 0x66;
        public const byte CHANGE_TEXTEMOTE_COLORS           = 0x69;
        public const byte TARGET_CURSOR_COMMANDS            = 0x6C;
        public const byte PLAY_MIDI_MUSIC                   = 0x6D;
        public const byte CHARACTER_ANIMATION               = 0x6E;
        public const byte SECURE_TRADING                    = 0x6F;
        public const byte GRAPHICAL_EFFECT                  = 0x70;
        public const byte BULLETIN_BOARD_MESSAGES           = 0x71;
        public const byte REQUEST_WAR_MODE                  = 0x72;
        public const byte PING_MESSAGE                      = 0x73;
        public const byte OPEN_BUY_WINDOW                   = 0x74;
        public const byte RENAME_CHARACTER                  = 0x75;
        public const byte NEW_SUBSERVER                     = 0x76;
        public const byte UPDATE_PLAYER                     = 0x77;
        public const byte DRAW_OBJECT                       = 0x78;
        public const byte OPEN_DIALOG_BOX                   = 0x7C;
        public const byte RESPONSE_TO_DIALOG_BOX            = 0x7D;
        public const byte LOGIN_REQUEST                     = 0x80;
        public const byte LOGIN_DENIED                      = 0x82;
        public const byte DELETE_CHARACTER                  = 0x83;
        public const byte RESEND_CHARACTERS_AFTER_DELETE    = 0x86;
        public const byte OPEN_PAPERDOLL                    = 0x88;
        public const byte CORPSE_CLOTHING                   = 0x89;
        public const byte CONNECT_TO_GAME_SERVER            = 0x8C;
        public const byte CHARACTER_CREATION_3D             = 0x8D;
        public const byte MAP_MESSAGE                       = 0x90;
        public const byte GAME_SERVER_LOGIN                 = 0x91;
        public const byte BOOK_HEADER_OLD                   = 0x93;
        public const byte DYE_WINDOW                        = 0x95;
        public const byte MOVE_PLAYER                       = 0x97;
        public const byte ALL_NAMES                         = 0x98;
        public const byte GIVE_BOATHOUSE_PLACEMENT_VIEW     = 0x99;
        public const byte CONSOLE_ENTRY_PROMPT              = 0x9A;
        public const byte REQUEST_HELP                      = 0x9B;
        public const byte REQUEST_ASSISTANCE                = 0x9C;
        public const byte SELL_LIST                         = 0x9E;
        public const byte SELL_LIST_REPLY                   = 0x9F;
        public const byte SELECT_SERVER                     = 0xA0;
        public const byte UPDATE_CURRENT_HEALTH             = 0xA1;
        public const byte UPDATE_CURRENT_MANA               = 0xA2;
        public const byte UPDATE_CURRENT_STAMINA            = 0xA3;
        public const byte CLIENT_SPY                        = 0xA4;
        public const byte OPEN_WEB_BROWSER                  = 0xA5;
        public const byte TIPNOTICE_WINDOW                  = 0xA6;
        public const byte REQUEST_TIPNOTICE_WINDOW          = 0xA7;
        public const byte SERVER_LIST                       = 0xA8;
        public const byte CHARACTERS__STARTING_LOCATIONS    = 0xA9;
        public const byte ALLOWREFUSE_ATTACK                = 0xAA;
        public const byte GUMP_TEXT_ENTRY_DIALOG            = 0xAB;
        public const byte GUMP_TEXT_ENTRY_DIALOG_REPLY      = 0xAC;
        public const byte UNICODEASCII_SPEECH_REQUEST       = 0xAD;
        public const byte UNICODE_SPEECH_MESSAGE            = 0xAE;
        public const byte DISPLAY_DEATH_ACTION              = 0xAF;
        public const byte SEND_GUMP_MENU_DIALOG             = 0xB0;
        public const byte GUMP_MENU_SELECTION               = 0xB1;
        public const byte CHAT_MESSAGE                      = 0xB2;
        public const byte CHAT_TEXT                         = 0xB3;
        public const byte OPEN_CHAT_WINDOW                  = 0xB5;
        public const byte SEND_HELPTIP_REQUEST              = 0xB6;
        public const byte HELPTIP_DATA                      = 0xB7;
        public const byte REQUESTCHAR_PROFILE               = 0xB8;
        public const byte SUPPORTED_FEATURES                = 0xB9;
        public const byte QUEST_ARROW                       = 0xBA;
        public const byte ULTIMA_MESSENGER                  = 0xBB;
        public const byte SEASONAL_INFORMATION              = 0xBC;
        public const byte CLIENT_VERSION                    = 0xBD;
        public const byte ASSIST_VERSION                    = 0xBE;
        public const byte GENERAL_INFORMATION_PACKET        = 0xBF;
        public const byte GRAPHICAL_EFFECT_2                = 0xC0;
        public const byte CLILOC_MESSAGE                    = 0xC1;
        public const byte UNICODE_TEXTENTRY                 = 0xC2;
        public const byte SEMIVISIBLE                       = 0xC4;
        public const byte INVALID_MAP                       = 0xC5;
        public const byte INVALID_MAP_ENABLE                = 0xC6;
        public const byte PARTICLE_EFFECT_3D                = 0xC7;
        public const byte CLIENT_VIEW_RANGE                 = 0xC8;
        public const byte GET_AREA_SERVER_PING              = 0xC9;
        public const byte GET_USER_SERVER_PING              = 0xCA;
        public const byte GLOBAL_QUE_COUNT                  = 0xCB;
        public const byte CLILOC_MESSAGE_AFFIX              = 0xCC;
        public const byte CONFIGURATION_FILE                = 0xD0;
        public const byte LOGOUT_STATUS                     = 0xD1;
        public const byte EXTENDED_0X20                     = 0xD2;
        public const byte EXTENDED_0X78                     = 0xD3;
        public const byte BOOK_HEADER_NEW                   = 0xD4;
        public const byte MEGA_CLILOC                       = 0xD6;
        public const byte GENERIC_AOS_COMMANDS              = 0xD7;
        public const byte SEND_CUSTOM_HOUSE                 = 0xD8;
        public const byte SPY_ON_CLIENT                     = 0xD9;
        public const byte CHARACTER_TRANSFER_LOG            = 0xDB;
        public const byte SE_INTRODUCED_REVISION            = 0xDC;
        public const byte COMPRESSED_GUMP                   = 0xDD;
        public const byte UPDATE_MOBILE_STATUS              = 0xDE;
        public const byte BUFFDEBUFF_SYSTEM                 = 0xDF;
        public const byte BUG_REPORT                        = 0xE0;
        public const byte CLIENT_TYPE                       = 0xE1;
        public const byte NEW_CHARACTER_ANIMATION           = 0xE2;
        public const byte KR_ENCRYPTION_RESPONSE            = 0xE3;
        public const byte EQUIP_MACRO                       = 0xEC;
        public const byte UNEQUIP_ITEM_MACRO                = 0xED;
        public const byte SEED                              = 0xEF;
        public const byte KRRIOS_CLIENT_SPECIAL             = 0xF0;
        public const byte FREESHARD_LIST                    = 0xF1;
        public const byte OBJECT_INFORMATION                = 0xF3;
        public const byte NEW_MAP_MESSAGE                   = 0xF5;
        public const byte CHARACTER_CREATION                = 0xF8;
        public const byte OPEN_UO_STORE                     = 0xFA;
        public const byte UPDATE_VIEW_PUBLIC_HOUSE_CONTENTS = 0xFB;

        public const byte UNKNOWN                           = 0xFF;
    }

    public static class PacketUtil
    {
        public static UOPacket.BaseUOPacket GetPacket(List<byte> bytes, Source source)
        {
            byte cmd = bytes[0];

            switch (cmd)
            {
                case CMD.SERVER_LIST:
                    return new UOPacket.ServerList(bytes);
                case CMD.SUPPORTED_FEATURES:
                    return new UOPacket.SupportedFeatures(bytes);
                case CMD.SEED:
                    return new UOPacket.Seed(bytes);
                case CMD.LOGIN_REQUEST:
                    return new UOPacket.LoginRequest(bytes);
                case CMD.SELECT_SERVER:
                    return new UOPacket.SelectServer(bytes);
                case CMD.CONNECT_TO_GAME_SERVER:
                    return new UOPacket.Connect(bytes);
                case CMD.ASSIST_VERSION:
                    return new UOPacket.AssistVersion(bytes);
                case CMD.CHAT_TEXT:
                    return new UOPacket.ChatText(bytes);
                case CMD.GAME_SERVER_LOGIN:
                    return new UOPacket.GameServerLogin(bytes);
                case CMD.KRRIOS_CLIENT_SPECIAL:
                    return new UOPacket.KrriosClientSpecial(bytes);
                default:
                    return new UOPacket.GenericPacket(bytes);
            }
        }

        // TODO: there's a chance with the length-variable packets that we
        // have the CMD byte, but we don't have the length bytes yet.. in 
        // that case we should probably return MAX_INT so that it's clear we
        // don't have all the data needed..
        public static int GetSize(List<byte> bytes, Source source)
        {
            byte cmd = bytes[0];

            switch (cmd)
            {
                case CMD.CREATE_CHARACTER:                  // 0x00
                    return 104;
                case CMD.DISCONNECT_NOTIFICATION:           // 0x01
                    return 5;
                case CMD.MOVE_REQUEST:                      // 0x02
                    return 7;
                case CMD.TALK_REQUEST:                      // 0x03
                    return GetVariableLength(1, bytes);
                case CMD.REQUEST_GOD_MODE:                  // 0x04
                    return 2;
                case CMD.REQUEST_ATTACK:                    // 0x05
                    return 5;
                case CMD.DOUBLE_CLICK:                      // 0x06
                    return 5;
                case CMD.PICK_UP_ITEM:                      // 0x07
                    return 7;
                case CMD.DROP_ITEM:                         // 0x08
                    return 15;
                case CMD.SINGLE_CLICK:                      // 0x09
                    return 5;
                case CMD.EDIT:                              // 0x0A
                    return 11;
                case CMD.DAMAGE:                            // 0x0B
                    return 7;
                case CMD.EDIT_TILE_DATA:                    // 0x0C
                    return GetVariableLength(1, bytes);
                case CMD.STATUS_BAR_INFO:                   // 0x11
                    return GetVariableLength(1, bytes);
                case CMD.REQUEST_SKILL_ETC_USE:             // 0x12
                    return GetVariableLength(1, bytes);
                case CMD.DROP_OR_WEAR_ITEM:                 // 0x13
                    return 10;
                case CMD.SEND_ELEVATION:                    // 0x14
                    return 6;
                case CMD.FOLLOW:                            // 0x15
                    return 9;
                case CMD.NEW_HEALTH_BAR_STATUS_UPDATE:      // 0x16
                    return GetVariableLength(1, bytes);
                case CMD.HEALTH_BAR_STATUS_UPDATE:          // 0x17
                    return 12;
                case CMD.OBJECT_INFO:                       // 0x1A
                    return GetVariableLength(1, bytes);
                case CMD.CHAR_LOCALE_AND_BODY:              // 0x1B
                    return 37;
                case CMD.SEND_SPEECH:                       // 0x1C
                    return GetVariableLength(1, bytes);
                case CMD.DELETE_OBJECT:                     // 0x1D
                    return 5;
                case CMD.CONTROL_ANIMATION:                 // 0x1E
                    return 4;
                case CMD.EXPLOSION:                         // 0x1F
                    return 8;
                case CMD.DRAW_GAME_PLAYER:                  // 0x20
                    return 19;
                case CMD.CHAR_MOVE_REJECTION:               // 0x21
                    return 8;
                case CMD.CHARACTER_MOVE_ACK_RESYNC_REQUEST: // 0x22
                    return 3;
                case CMD.DRAGGING_OF_ITEM:                  // 0x23
                    return 26;
                case CMD.DRAW_CONTAINER:                    // 0x24
                    return 7;
                case CMD.ADD_ITEM_TO_CONTAINER:             // 0x25
                    return 21;
                case CMD.KICK_PLAYER:                       // 0x26
                    return 5;
                case CMD.REJECT_MOVE_ITEM_REQUEST:          // 0x27
                    return 2;
                case CMD.DROP_ITEM_FAILEDCLEAR_SQUARE:      // 0x28
                    return 5;
                case CMD.DROP_ITEM_APPROVED:                // 0x29
                    return 1;
                case CMD.BLOOD:                             // 0x2A
                    return 5;
                case CMD.GOD_MODE:                          // 0x2B
                    return 2;
                case CMD.RESURRECTION_MENU:                 // 0x2C
                    return 2;
                case CMD.MOB_ATTRIBUTES:                    // 0x2D
                    return 17;
                case CMD.WORN_ITEM:                         // 0x2E
                    return 15;
                case CMD.FIGHT_OCCURING:                    // 0x2F
                    return 10;
                case CMD.ATTACK_OK:                         // 0x30
                    return 5;
                case CMD.ATTACK_ENDED:                      // 0x31
                    return 1;
                case CMD.UNKNOWNX32:                        // 0x32
                    return 2;
                case CMD.PAUSE_CLIENT:                      // 0x33
                    return 2;
                case CMD.GET_PLAYER_STATUS:                 // 0x34
                    return 10;
                case CMD.ADD_RESOURCE:                      // 0x35
                    return 653;
                case CMD.RESOURCE_TILE_DATA:                // 0x36
                    return GetVariableLength(1, bytes);
                case CMD.MOVE_ITEM:                         // 0x37
                    return 8;
                case CMD.PATHFINDING_IN_CLIENT:             // 0x38
                    return 7;
                case CMD.REMOVE:                            // 0x39
                    return 9;
                case CMD.SEND_SKILLS:                       // 0x3A
                    return GetVariableLength(1, bytes);
                case CMD.BUY_ITEM:                          // 0x3B
                    return GetVariableLength(1, bytes);
                case CMD.ADD_MULTIPLE_ITEMS_IN_CONTAINER:   // 0x3C
                    return GetVariableLength(1, bytes);
                case CMD.VERSIONS:                          // 0x3E
                    return 37;
                case CMD.UPDATE_STATICS:                    // 0x3F
                    return GetVariableLength(1, bytes);
                case CMD.VERSION_OK:                        // 0x45
                    return 5;
                case CMD.NEW_ARTWORK:                       // 0x46
                    return GetVariableLength(1, bytes);
                case CMD.NEW_TERRAIN:                       // 0x47
                    return 11;
                case CMD.NEW_ANIMATION:                     // 0x48
                    return 73;
                case CMD.NEW_HUES:                          // 0x49
                    return 93;
                case CMD.DELETE_ART:                        // 0x4A
                    return 5;
                case CMD.CHECK_CLIENT_VERSION:              // 0x4B
                    return 9;
                case CMD.SCRIPT_NAMES:                      // 0x4C
                    return GetVariableLength(1, bytes);
                case CMD.EDIT_SCRIPT_FILE:                  // 0x4D
                    return GetVariableLength(1, bytes);
                case CMD.PERSONAL_LIGHT_LEVEL:              // 0x4E
                    return 6;
                case CMD.OVERALL_LIGHT_LEVEL:               // 0x4F
                    return 2;
                case CMD.BOARD_HEADER:                      // 0x50
                    return GetVariableLength(1, bytes);
                case CMD.BOARD_MESSAGE:                     // 0x51
                    return GetVariableLength(1, bytes);
                case CMD.BOARD_POST_MESSAGE:                // 0x52
                    return GetVariableLength(1, bytes);
                case CMD.REJECT_CHARACTER_LOGON:            // 0x53
                    return 2;
                case CMD.PLAY_SOUND_EFFECT:                 // 0x54
                    return 12;
                case CMD.LOGIN_COMPLETE:                    // 0x55
                    return 1;
                case CMD.MAP_PACKET:                        // 0x56
                    return 11;
                case CMD.UPDATE_REGIONS:                    // 0x57
                    return 110;
                case CMD.ADD_REGION:                        // 0x58
                    return 106;
                case CMD.NEW_CONTEXT_FX:                    // 0x59
                    return GetVariableLength(1, bytes);
                case CMD.UPDATE_CONTEXT_FX:                 // 0x5A
                    return GetVariableLength(1, bytes);
                case CMD.TIME:                              // 0x5B
                    return 4;
                case CMD.RESTART_VERSION:                   // 0x5C
                    return 2;
                case CMD.LOGIN_CHARACTER:                   // 0x5D
                    return 73;
                case CMD.SERVER_LISTING:                    // 0x5E
                    return GetVariableLength(1, bytes);
                case CMD.SERVER_LIST_ADD_ENTRY:             // 0x5F
                    return 49;
                case CMD.SERVER_LIST_REMOVE_ENTRY:          // 0x60
                    return 5;
                case CMD.REMOVE_STATIC_OBJECT:              // 0x61
                    return 9;
                case CMD.MOVE_STATIC_OBJECT:                // 0x62
                    return 15;
                case CMD.LOAD_AREA:                         // 0x63
                    return 13;
                case CMD.LOAD_AREA_REQUEST:                 // 0x64
                    return 1;
                case CMD.SET_WEATHER:                       // 0x65
                    return 4;
                case CMD.BOOKS:                             // 0x66
                    return GetVariableLength(1, bytes);
                case CMD.CHANGE_TEXTEMOTE_COLORS:           // 0x69
                    return 5;
                case CMD.TARGET_CURSOR_COMMANDS:            // 0x6C
                    return 19;
                case CMD.PLAY_MIDI_MUSIC:                   // 0x6D
                    return 3;
                case CMD.CHARACTER_ANIMATION:               // 0x6E
                    return 14;
                case CMD.SECURE_TRADING:                    // 0x6F
                    return GetVariableLength(1, bytes);
                case CMD.GRAPHICAL_EFFECT:                  // 0x70
                    return 28;
                case CMD.BULLETIN_BOARD_MESSAGES:           // 0x71
                    return GetVariableLength(1, bytes);
                case CMD.REQUEST_WAR_MODE:                  // 0x72
                    return 5;
                case CMD.PING_MESSAGE:                      // 0x73
                    return 2;
                case CMD.OPEN_BUY_WINDOW:                   // 0x74
                    return GetVariableLength(1, bytes);
                case CMD.RENAME_CHARACTER:                  // 0x75
                    return 35;
                case CMD.NEW_SUBSERVER:                     // 0x76
                    return 16;
                case CMD.UPDATE_PLAYER:                     // 0x77
                    return 17;
                case CMD.DRAW_OBJECT:                       // 0x78
                    return GetVariableLength(1, bytes);
                case CMD.OPEN_DIALOG_BOX:                   // 0x7C
                    return GetVariableLength(1, bytes);
                case CMD.RESPONSE_TO_DIALOG_BOX:            // 0x7D
                    return 13;
                case CMD.LOGIN_REQUEST:                     // 0x80
                    return 62;
                case CMD.LOGIN_DENIED:                      // 0x82
                    return 2;
                case CMD.DELETE_CHARACTER:                  // 0x83
                    return 39;
                case CMD.RESEND_CHARACTERS_AFTER_DELETE:    // 0x86
                    return 304;
                case CMD.OPEN_PAPERDOLL:                    // 0x88
                    return 66;
                case CMD.CORPSE_CLOTHING:                   // 0x89
                    return GetVariableLength(1, bytes);
                case CMD.CONNECT_TO_GAME_SERVER:            // 0x8C
                    return 11;
                case CMD.CHARACTER_CREATION_3D:             // 0x8D
                    return 146;
                case CMD.MAP_MESSAGE:                       // 0x90
                    return 19;
                case CMD.GAME_SERVER_LOGIN:                 // 0x91
                    return 65;
                case CMD.BOOK_HEADER_OLD:                   // 0x93
                    return 99;
                case CMD.DYE_WINDOW:                        // 0x95
                    return 9;
                case CMD.MOVE_PLAYER:                       // 0x97
                    return 2;
                case CMD.ALL_NAMES:                         // 0x98
                    return GetVariableLength(1, bytes);
                case CMD.GIVE_BOATHOUSE_PLACEMENT_VIEW:     // 0x99
                    return 26;
                case CMD.CONSOLE_ENTRY_PROMPT:              // 0x9A
                    return GetVariableLength(1, bytes);
                case CMD.REQUEST_HELP:                      // 0x9B
                    return 258;
                case CMD.REQUEST_ASSISTANCE:                // 0x9C
                    return 53;
                case CMD.SELL_LIST:                         // 0x9E
                    return GetVariableLength(1, bytes);
                case CMD.SELL_LIST_REPLY:                   // 0x9F
                    return GetVariableLength(1, bytes);
                case CMD.SELECT_SERVER:                     // 0xA0
                    return 3;
                case CMD.UPDATE_CURRENT_HEALTH:             // 0xA1
                    return 9;
                case CMD.UPDATE_CURRENT_MANA:               // 0xA2
                    return 9;
                case CMD.UPDATE_CURRENT_STAMINA:            // 0xA3
                    return 9;
                case CMD.CLIENT_SPY:                        // 0xA4
                    return GetVariableLength(1, bytes);
                case CMD.OPEN_WEB_BROWSER:                  // 0xA5
                    return GetVariableLength(1, bytes);
                case CMD.TIPNOTICE_WINDOW:                  // 0xA6
                    return GetVariableLength(1, bytes);
                case CMD.REQUEST_TIPNOTICE_WINDOW:          // 0xA7
                    return 4;
                case CMD.SERVER_LIST:                       // 0xA8
                    return GetVariableLength(1, bytes);
                case CMD.CHARACTERS__STARTING_LOCATIONS:    // 0xA9
                    return GetVariableLength(1, bytes);
                case CMD.ALLOWREFUSE_ATTACK:                // 0xAA
                    return 5;
                case CMD.GUMP_TEXT_ENTRY_DIALOG:            // 0xAB
                    return GetVariableLength(1, bytes);
                case CMD.GUMP_TEXT_ENTRY_DIALOG_REPLY:      // 0xAC
                    return GetVariableLength(1, bytes);
                case CMD.UNICODEASCII_SPEECH_REQUEST:       // 0xAD
                    return GetVariableLength(1, bytes);
                case CMD.UNICODE_SPEECH_MESSAGE:            // 0xAE
                    return GetVariableLength(1, bytes);
                case CMD.DISPLAY_DEATH_ACTION:              // 0xAF
                    return 13;
                case CMD.SEND_GUMP_MENU_DIALOG:             // 0xB0
                    return GetVariableLength(1, bytes);
                case CMD.GUMP_MENU_SELECTION:               // 0xB1
                    return GetVariableLength(1, bytes);
                case CMD.CHAT_MESSAGE:                      // 0xB2
                    return GetVariableLength(1, bytes);
                case CMD.CHAT_TEXT:                         // 0xB3
                    return GetVariableLength(1, bytes);
                case CMD.OPEN_CHAT_WINDOW:                  // 0xB5
                    return 64;
                case CMD.SEND_HELPTIP_REQUEST:              // 0xB6
                    return 9;
                case CMD.HELPTIP_DATA:                      // 0xB7
                    return GetVariableLength(1, bytes);
                case CMD.REQUESTCHAR_PROFILE:               // 0xB8
                    return GetVariableLength(1, bytes);
                case CMD.SUPPORTED_FEATURES:                // 0xB9
                    return 5;
                case CMD.QUEST_ARROW:                       // 0xBA
                    return 6;
                case CMD.ULTIMA_MESSENGER:                  // 0xBB
                    return 9;
                case CMD.SEASONAL_INFORMATION:              // 0xBC
                    return 3;
                case CMD.CLIENT_VERSION:                    // 0xBD
                    return GetVariableLength(1, bytes);
                case CMD.ASSIST_VERSION:                    // 0xBE
                    return GetVariableLength(1, bytes);
                case CMD.GENERAL_INFORMATION_PACKET:        // 0xBF
                    return GetVariableLength(1, bytes);
                case CMD.GRAPHICAL_EFFECT_2:                // 0xC0
                    return 36;
                case CMD.CLILOC_MESSAGE:                    // 0xC1
                    return GetVariableLength(1, bytes);
                case CMD.UNICODE_TEXTENTRY:                 // 0xC2
                    return GetVariableLength(1, bytes);
                case CMD.SEMIVISIBLE:                       // 0xC4
                    return 6;
                case CMD.INVALID_MAP:                       // 0xC5
                    return 1;
                case CMD.INVALID_MAP_ENABLE:                // 0xC6
                    return 1;
                case CMD.PARTICLE_EFFECT_3D:                // 0xC7
                    return 49;
                case CMD.CLIENT_VIEW_RANGE:                 // 0xC8
                    return 2;
                case CMD.GET_AREA_SERVER_PING:              // 0xC9
                    return 6;
                case CMD.GET_USER_SERVER_PING:              // 0xCA
                    return 6;
                case CMD.GLOBAL_QUE_COUNT:                  // 0xCB
                    return 7;
                case CMD.CLILOC_MESSAGE_AFFIX:              // 0xCC
                    return GetVariableLength(1, bytes);
                case CMD.CONFIGURATION_FILE:                // 0xD0
                    return GetVariableLength(1, bytes);
                case CMD.LOGOUT_STATUS:                     // 0xD1
                    return 2;
                case CMD.EXTENDED_0X20:                     // 0xD2
                    return 25;
                case CMD.EXTENDED_0X78:                     // 0xD3
                    return GetVariableLength(1, bytes);
                case CMD.BOOK_HEADER_NEW:                   // 0xD4
                    return GetVariableLength(1, bytes);
                case CMD.MEGA_CLILOC:                       // 0xD6
                    return GetVariableLength(1, bytes);
                case CMD.GENERIC_AOS_COMMANDS:              // 0xD7
                    return GetVariableLength(1, bytes);
                case CMD.SEND_CUSTOM_HOUSE:                 // 0xD8
                    return GetVariableLength(1, bytes);
                case CMD.SPY_ON_CLIENT:                     // 0xD9
                    return GetVariableLength(1, bytes);
                case CMD.CHARACTER_TRANSFER_LOG:            // 0xDB
                    return GetVariableLength(1, bytes);
                case CMD.SE_INTRODUCED_REVISION:            // 0xDC
                    return 9;
                case CMD.COMPRESSED_GUMP:                   // 0xDD
                    return GetVariableLength(1, bytes);
                case CMD.UPDATE_MOBILE_STATUS:              // 0xDE
                    return GetVariableLength(1, bytes);
                case CMD.BUFFDEBUFF_SYSTEM:                 // 0xDF
                    return GetVariableLength(1, bytes);
                case CMD.BUG_REPORT:                        // 0xE0
                    return GetVariableLength(1, bytes);
                case CMD.CLIENT_TYPE:                       // 0xE1
                    return 9;
                case CMD.NEW_CHARACTER_ANIMATION:           // 0xE2
                    return 10;
                case CMD.KR_ENCRYPTION_RESPONSE:            // 0xE3
                    return 77;
                case CMD.EQUIP_MACRO:                       // 0xEC
                    return GetVariableLength(1, bytes);
                case CMD.UNEQUIP_ITEM_MACRO:                // 0xED
                    return GetVariableLength(1, bytes);
                case CMD.SEED:                              // 0xEF
                    return 21;
                case CMD.KRRIOS_CLIENT_SPECIAL:             // 0xF0
                    return GetVariableLength(1, bytes);
                case CMD.FREESHARD_LIST:                    // 0xF1
                    return GetVariableLength(1, bytes);
                case CMD.OBJECT_INFORMATION:                // 0xF3
                    return 26;
                case CMD.NEW_MAP_MESSAGE:                   // 0xF5
                    return 21;
                case CMD.CHARACTER_CREATION:                // 0xF8
                    return 106;
                case CMD.OPEN_UO_STORE:                     // 0xFA
                    return 1;
                case CMD.UPDATE_VIEW_PUBLIC_HOUSE_CONTENTS: // 0xFB
                    return 2;
                default:
                    return 0;
            }
        }

        private static int GetVariableLength(int index, List<byte> bytes)
        {
            if (index + 2 > bytes.Count)
                return int.MaxValue;

            List<byte> lengthBytes = bytes.GetRange(index, 2);
            lengthBytes.Reverse();
            byte[] bigEndianBytes = lengthBytes.ToArray();
            short size = BitConverter.ToInt16(bigEndianBytes);

            return (int)size;
        }
    }
}
